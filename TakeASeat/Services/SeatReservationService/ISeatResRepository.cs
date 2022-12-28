using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.SeatReservationService
{
    public interface ISeatResRepository
    {
        Task CreateSeatReservation(string userId, IEnumerable<Seat> seats);
        Task DeleteSeatReservation(IEnumerable<SeatReservation> seatReservation);
    }
}
