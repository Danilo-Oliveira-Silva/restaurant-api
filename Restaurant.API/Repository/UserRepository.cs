namespace Restaurant.API.Repository;

using MongoDB.Driver;
using Restaurant.API.Context;
using Restaurant.API.Models;
using Restaurant.API.DTO;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserRepository() {
         var mongoDatabase = ContextConnection.GetDatabase();
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }
    
    public User Signup(User newUser)
    {
        _usersCollection.InsertOne(newUser);
        return newUser;
    }
    public User Login(LoginDto login)
    {
        var userFinded = _usersCollection.Find(u => u.Email == login.Email).ToList();
        User userReturned = default!;
        if (userFinded.Count() > 0) userReturned = userFinded.First();
        if (userReturned.Password != login.Password) throw new AccessViolationException("Login inválido");
        return userReturned!;
    }
    
    public User GetUser(string email)
    {
        var userFinded = _usersCollection.Find(u => u.Email == email).ToList();
        User userReturned = default!;
        if (userFinded.Count() > 0) userReturned = userFinded.First();
        return userReturned!;
    }
}
