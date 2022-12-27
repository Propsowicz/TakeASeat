using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using Microsoft.EntityFrameworkCore;


namespace TakeASeat.Services.BackgroundService
{
    public class ReleaseReservationService : IReleaseReservationService
    {
        private readonly DatabaseContext _context;       

        public ReleaseReservationService(DatabaseContext context, IServiceProvider serviceProvider)
        {
            _context = context;
        }

        public async Task ReleaseUnpaidReservations()
        {
            var seatsQuery = await _context.Seats
                            .Where(s => s.Show.Date > DateTime.UtcNow
                            && s.isReserved.Equals(true)
                            && s.isSold.Equals(false))
                            .ToListAsync();

            List<int?> seatsIdList = new List<int?>() { };

            foreach (var seat in seatsQuery)
            {
                if ((DateTime.UtcNow - seat.ReservedTime).TotalMinutes > 1)
                {
                    seatsIdList.Add(seat.Id);
                    seat.isReserved = false;
                    seat.ReservedTime = new DateTime(0001, 01, 01, 0, 0, 0);
                }
            }
            await _context.SaveChangesAsync();
            var seatsResQuery = await _context.SeatReservation
                            .Where(sr => seatsIdList.Contains(sr.SeatId))
                            .ToListAsync();
            _context.SeatReservation.RemoveRange(seatsResQuery);
        }

    }
}
