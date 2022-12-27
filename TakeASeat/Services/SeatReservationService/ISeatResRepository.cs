using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.SeatReservationService
{
    public interface ISeatResRepository
    {
        Task CreateSeatReservations(string buyerId, int eventId, IEnumerable<Seat> seats);
    }
}
