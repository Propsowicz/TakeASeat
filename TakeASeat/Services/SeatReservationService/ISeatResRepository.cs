using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.SeatReservationService
{
    public interface ISeatResRepository
    {
        Task CreateSeatReservation(string userId, IEnumerable<Seat> seats);
        Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations);

        Task DeleteSeatReservation(int seatReservationId);
        Task SetReservation(IEnumerable<Seat> seats, int? ReservationId);
        Task RemoveReservation(int reservationId);
        Task RemoveSingleSeatFromOrder(int reservationId);
        Task RemoveMultipleReservation(List<int> reservationIds);
    }

}
