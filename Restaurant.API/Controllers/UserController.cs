using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Restaurant.API.Models;
using Restaurant.API.DTO;
using Restaurant.API.Repository;
using Restaurant.API.Services;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    
    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        var newUser = _repository.Signup(user);
        var token = new TokenGenerator().Generate(newUser);
        return Created("", new { token });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDTO)
    {
        try
        {
            var user = _repository.Login(loginDTO);
            var token = new TokenGenerator().Generate(user);
            return Ok(new { token });
        }
        catch(Exception ex)
        {
            return Unauthorized(new { message = ex.Message.ToString() });
        }
        
    }
}