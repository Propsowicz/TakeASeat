using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;


namespace TakeASeat.Services.BackgroundService
{
    public class ReleaseReservationService : IReleaseReservationService
    {
        private readonly DatabaseContext _context;
        private readonly ISeatResRepository _seatReservationRepository;
        public ReleaseReservationService(DatabaseContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _seatReservationRepository = serviceProvider.GetRequiredService<ISeatResRepository>();
        }

        public async Task ReleaseUnpaidReservations()
        {
            var seatReservationQuery = await _context.SeatReservation
                                            .Where(r => r.isReserved == true
                                            && r.isSold == false
                                            && r.ReservedTime.Minute + 1 < DateTime.UtcNow.Minute)      // here shoul be 5 minutes added
                                            .Include(r => r.Seats)
                                            .ToListAsync();

            if (seatReservationQuery.Count > 0 )
            {
                await _seatReservationRepository.DeleteSeatReservation(seatReservationQuery);
            }
            
        }

    }
}
