using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.SeatReservationService
{
    public interface ISeatResRepository
    {
        Task CreateSeatReservation(string userId, IEnumerable<Seat> seats);
        Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations);
        Task DeleteSeatReservation(int seatReservationId);
    }
}
