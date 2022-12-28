using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using Microsoft.EntityFrameworkCore;


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
                                            && r.isSold == false)
                                            .Include(r => r.Seats)
                                            .ToListAsync();
            if (seatReservationQuery.Count > 0 )
            {
                await _seatReservationRepository.DeleteSeatReservation(seatReservationQuery);
            }
            
            //foreach (var reservation in seatReservationQuery)
            //{
            //    var listOfSeats = reservation.Seats.ToList();
            //    foreach (var seat in listOfSeats)
            //    {
            //        seat.ReservationId = null;
            //    }
            //    await _context.SaveChangesAsync();
            //    _context.Remove(reservation);
            //}         

        }

    }
}
