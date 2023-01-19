using TakeASeat.Data;

namespace TakeASeat.Services.SeatReservationService
{
    public interface ISeatResRepository
    {
        Task CreateSeatReservation(string userId, IEnumerable<Seat> seats);
        Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations);
        Task DeleteSeatReservation(int seatReservationId);
        Task AddReservationToMultipleSeats(IEnumerable<Seat> seats, int? ReservationId);        
        Task RemoveReservationFromSeat(int reservationId); 
        Task RemoveReservationFromMultipleSeats(int reservationId); 
        Task RemoveMultipleReservationsFromMultipleSeats(List<int> reservationIds);
    }

}
