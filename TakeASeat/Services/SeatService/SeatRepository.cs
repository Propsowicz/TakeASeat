using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using X.PagedList;

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
            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();
            
        }       

        public async Task<IList<Seat[]>> GetSeats(int showId)
        {
            //return await _context.Seats
            //    .Where(s => s.ShowId == showId)
            //    .ToListAsync();
            return await _context.Seats
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }
                
    }
}
