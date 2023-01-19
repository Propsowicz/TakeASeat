using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services._Utils;
using TakeASeat.Services.SeatService;

namespace TakeASeat.Services.SeatReservationService
{
    public class SeatResRepository : ISeatResRepository
    {
        private readonly DatabaseContext _context;
        public SeatResRepository(DatabaseContext context)
        {
            _context= context;
        }
        public async Task CreateSeatReservation(string userId, IEnumerable<Seat> seats)
        {
            if (userId == null || userId == string.Empty)
            {
                return;
            }
            var reservation = await _context.SeatReservation
                .AddAsync(new SeatReservation
                {
                    isReserved = true,
                    ReservedTime= DateTime.UtcNow,
                    UserId= userId,
                });
            await _context.SaveChangesAsync();
            await AddReservationToMultipleSeats(seats, reservation.Entity.Id);
        }       
        public async Task DeleteSeatReservation(int seatReservationId)
        {
            var query = await _context.SeatReservation
                                .Where(r => r.Id == seatReservationId)
                                .FirstOrDefaultAsync(); 
                        
            await RemoveReservationFromMultipleSeats(seatReservationId);

            ArgumentNullException.ThrowIfNull(query);
            _context.Remove(query);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations)
        {
            
            var listOfReservations = seatReservations.Select(r => r.Id).ToList();
            await RemoveMultipleReservationsFromMultipleSeats(listOfReservations);

            _context.RemoveRange(seatReservations);
            await _context.SaveChangesAsync();
        }

        public async void DeleteEmptyReservation(int? seatReservationId)
        {
            var reservation = _context.SeatReservation
                                .Include(r => r.Seats)
                                .FirstOrDefault(r => r.Id == seatReservationId);

            ArgumentNullException.ThrowIfNull(reservation);

            if (reservation.Seats.Count == 0)
            {
                _context.SeatReservation.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IList<Seat[]>> GetSeats(int showId)
        {
            return await _context.Seats
                    .AsNoTracking()
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }
        public async Task RemoveMultipleReservationsFromMultipleSeats(List<int> reservationIds)
        {                                                                                                                         
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE Seats SET ReservationId = NULL WHERE {RawSqlHelper.WHERE_ReservationId_is_Id(reservationIds)}"
                );
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RemoveReservationFromMultipleSeats(int reservationId)
        {
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE Seats SET ReservationId = NULL WHERE ReservationId = {reservationId}"
                );
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RemoveReservationFromSeat(int seatId)
        {
            if (seatId < 1) { return; }
            var seat = await _context.Seats
                    .FirstOrDefaultAsync(s => s.Id == seatId);
            var reservationId = seat.ReservationId;
            ArgumentNullException.ThrowIfNull(seat);
            seat.ReservationId = null;
            await _context.SaveChangesAsync();

            // DeleteEmptyReservation() checks if lastly deleted seat was the only seat left in the reservation.
            // If it was (so there is no more seats) -> delete the reservation
            DeleteEmptyReservation(reservationId);
        }
        public async Task AddReservationToMultipleSeats(IEnumerable<Seat> seats, int? ReservationId)
        {                                                                                                                      
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE Seats SET ReservationId = {ReservationId} WHERE {RawSqlHelper.WHERE_Id_is_SeatId(seats)}"
                );
            await _context.Database.CommitTransactionAsync();
        }
    }
}
