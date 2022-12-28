using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using X.PagedList;

namespace TakeASeat.Services.SeatService
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DatabaseContext _context;             

        public SeatRepository(DatabaseContext context, IServiceProvider serviceProvider)
        {
            _context= context;            
        }

        public async Task CreateMultipleSeats(IEnumerable<Seat> seats)
        {
            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();
            
        }       

        public async Task<IList<Seat[]>> GetSeats(int showId)
        {            
            return await _context.Seats
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }

        

        public async Task SetReservation(IEnumerable<Seat> seats, int? ReservationId)
        {            
            foreach (var seat in seats)
            {
                var _ = await _context.Seats.FindAsync(seat.Id);
                _.ReservationId = ReservationId;                
            }
            await _context.SaveChangesAsync();
        }
    }
}
