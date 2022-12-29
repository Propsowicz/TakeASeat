using TakeASeat.Data;

namespace TakeASeat.Services.SeatService
{
    public interface ISeatRepository
    {
        Task<IList<Seat[]>> GetSeats(int showId);
        Task CreateMultipleSeats(IEnumerable<Seat> seats);
        Task SetReservation(IEnumerable<Seat> seats, int? ReservationId);
        Task RemoveReservation(int reservationId);
        Task RemoveSingleReservation(int reservationId);
        Task RemoveMultipleReservation(List<int> reservationIds);
    }
}
