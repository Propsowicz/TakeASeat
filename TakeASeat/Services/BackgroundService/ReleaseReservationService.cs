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
            var dateTime = DateTime.UtcNow;
            var seatReservationQuery = await _context.SeatReservation
                                            .Where(r => 
                                            r.isReserved == true
                                            && r.isSold == false)
                                            .Where(r => 
                                            r.ReservedTime.Hour <= dateTime.Hour
                                            && r.ReservedTime.Minute + 5 < dateTime.Minute 
                                            || dateTime.Minute < r.ReservedTime.Minute - 5)                                            
                                            .ToListAsync();

            if (seatReservationQuery.Count > 0 )
            {
                await _seatReservationRepository.DeleteSeatReservations(seatReservationQuery);
                Console.WriteLine("Unpaid reservations has been deleted...");
            }
            
        }

    }
}
