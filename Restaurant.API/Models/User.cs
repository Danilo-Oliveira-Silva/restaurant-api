namespace Restaurant.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}