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
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _repository;
    private readonly IUserRepository _userRepository;
    
    public ReservationsController(IReservationRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Client")]
    public IActionResult Create([FromBody] ReservationDTO reservationDTO)
    {
        try
        {
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            User userFinded = _userRepository.GetUser(email!);
            Reservation newReservation = new Reservation { User = userFinded.Guid!, Date = reservationDTO.Date, GuestQuant = reservationDTO.GuestQuant };
            Reservation reservationCreated = _repository.Create(newReservation);
            return Created("", reservationCreated);
        } catch(Exception ex)
        {
            return BadRequest(new { message = "Error on create"});
        }
    }

    [HttpGet("{date}")]
    public IActionResult List(string date)
    {
        try
        {
            var reservations = _repository.Get(Convert.ToDateTime(date));
            return Ok(reservations);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = "Error on list"});
        }
    }

    [HttpDelete("{ReservationId}")]
    public IActionResult Delete(string ReservationId)
    {
        try
        {
            _repository.Delete(ReservationId);
            return NoContent();
        } catch(Exception ex)
        {
            return BadRequest(new { message = "Error on delete"});
        }
    }
}