using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using X.PagedList;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.IdentityModel.Tokens;

namespace TakeASeat.Services.SeatService
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DatabaseContext _context;

        public SeatRepository(DatabaseContext context)
        {
            _context= context;   
        }

        public async Task CreateMultipleSeats(IEnumerable<Seat> seats)
        {
            if (seats.IsNullOrEmpty())
            {
                return;
            }
            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();
            
        }

        //public void DeleteEmptyReservation(int seatReservationId)
        //{
        //    var reservation = _context.SeatReservation
        //                        .Include(r => r.Seats)
        //                        .FirstOrDefault(r => r.Id == seatReservationId);

        //    ArgumentNullException.ThrowIfNull(reservation);

        //    if (reservation.Seats.Count == 0)
        //    {
        //        _context.SeatReservation.Remove(reservation);
        //    }
        //}

        public async Task<IList<Seat[]>> GetSeats(int showId)
        {           
            return await _context.Seats
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }

        //public async Task RemoveMultipleReservation(List<int> reservationIds)
        //{
        //    string WhereConditions = string.Empty;            
        //    for (var i = 0; i < reservationIds.Count; i++)
        //    {                
        //        if (i == reservationIds.Count - 1)
        //        {
        //            WhereConditions += $"ReservationId = {reservationIds[i]}";
        //        }
        //        else
        //        {
        //            WhereConditions += $"ReservationId = {reservationIds[i]} OR ";
        //        }
        //    }

        //    await _context.Database.BeginTransactionAsync();
        //    await _context.Database.ExecuteSqlRawAsync(
        //        $"UPDATE Seats SET ReservationId = NULL WHERE {WhereConditions}"
        //        );
        //    await _context.Database.CommitTransactionAsync();
        //}

        //public async Task RemoveReservation(int reservationId)
        //{
        //    await _context.Database.BeginTransactionAsync();
        //    await _context.Database.ExecuteSqlInterpolatedAsync(
        //        $"UPDATE Seats SET ReservationId = NULL WHERE ReservationId = {reservationId}"
        //        );
        //    await _context.Database.CommitTransactionAsync();
        //}

        //public async Task RemoveSingleReservation(int reservationId)
        //{
        //    var seat = await _context.Seats
        //            .Where(s => s.Id== reservationId)
        //            .FirstOrDefaultAsync();

        //    ArgumentNullException.ThrowIfNull(seat);
        //    seat.ReservationId = null;
        //    await _context.SaveChangesAsync();
        //    DeleteEmptyReservation(reservationId);

        //}

        //public async Task SetReservation(IEnumerable<Seat> seats, int? ReservationId)
        //{       
        //    // need to be rawSQL
        //    foreach (var seat in seats)
        //    {
        //        var _ = await _context.Seats.FindAsync(seat.Id);
        //        _.ReservationId = ReservationId;                
        //    }
        //    await _context.SaveChangesAsync();
        //}
    }
}
