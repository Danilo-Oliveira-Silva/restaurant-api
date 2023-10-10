namespace Restaurant.API.Repository;

using Restaurant.API.Models;
using Restaurant.API.DTO;
public interface IUserRepository
{
    User Signup(User newUser);
    User Login(LoginDto user);
    User GetUser(string email);
}