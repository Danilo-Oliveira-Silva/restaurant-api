namespace Restaurant.API.Repository;

using Restaurant.API.Models;

public interface IReservationRepository
{
    Reservation Create(Reservation reservation);
    List<Reservation> Get(DateTime date);
    void Delete(string ReservationId);
}