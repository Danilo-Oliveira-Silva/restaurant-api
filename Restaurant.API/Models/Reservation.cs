namespace Restaurant.API.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Guid { get; set; }
    public string User { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public int? GuestQuant { get; set; }
}