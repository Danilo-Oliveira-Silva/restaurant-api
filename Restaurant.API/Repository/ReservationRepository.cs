namespace Restaurant.API.Repository;

using MongoDB.Driver;
using Restaurant.API.Context;
using Restaurant.API.Models;

public class ReservationRepository : IReservationRepository
{
     private readonly IMongoCollection<Reservation> _reservationCollection;

    public ReservationRepository() {
         var mongoDatabase = ContextConnection.GetDatabase();
        _reservationCollection = mongoDatabase.GetCollection<Reservation>("Reservation");
    }
    public Reservation Create(Reservation reservation)
    {
        _reservationCollection.InsertOne(reservation);
        return reservation;
    }
    
    public List<Reservation> Get(DateTime date)
    {
        var reservationList = _reservationCollection.Find(r => r.Date == date).ToList();
        return reservationList!;
    }
    
    public void Delete(string ReservationId)
    {
        _reservationCollection.DeleteOne(r => r.Guid == ReservationId);
    }
}