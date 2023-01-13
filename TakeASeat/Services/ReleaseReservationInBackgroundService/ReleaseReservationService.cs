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
        public ReleaseReservationService(DatabaseContext context, ISeatResRepository seatReservationRepository)
        {
            _context = context;
            _seatReservationRepository = seatReservationRepository;
        }
        
        public async Task<string> ReleaseUnpaidReservations()
        {            
            var dateTime = DateTime.UtcNow.AddMinutes(-5);
           ;
            var seatReservationQuery = await _context.SeatReservation
                                            .Where(r =>
                                            r.isReserved == true
                                            && r.isSold == false)
                                            .Where(r =>
                                            r.ReservedTime < dateTime)
                                            .ToListAsync();

            if (seatReservationQuery.Count > 0 )
            {
                await _seatReservationRepository.DeleteSeatReservations(seatReservationQuery);
                return "Unpaid reservations has been deleted...";
            }
            return "No unpaid reservations has been found...";
            
        }

    }
}
