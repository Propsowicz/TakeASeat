
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.BackgroundService;
using TakeASeat.Services.SeatService;

namespace TakeASeat.BackgroundServices
{
    public class ReleaseReservation : BackgroundService
    {
        private readonly DatabaseContext _context;
        private readonly ISeatRepository _seatRepository;
        private readonly IReleaseReservationService _reservationReleaseRepository;
        public ReleaseReservation(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
            //_seatRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISeatRepository>();
            _reservationReleaseRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IReleaseReservationService>();
        }

        private const int generalDelay = 1 * 20000; // 2 minutes
        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, stopToken);
                await UnpaidReservationCleaner();                
            }
        }

        public async Task UnpaidReservationCleaner()
        {
            Console.WriteLine("Cleaning up unpaid reservations..");
            await _reservationReleaseRepository.ReleaseUnpaidReservations();



            //var seatsList = await _context.Seats
            //                .Where(s => s.Show.Date > DateTime.UtcNow
            //                && s.isReserved.Equals(true)
            //                && s.isSold.Equals(false))
            //                .ToListAsync();

            //foreach (var seat in seatsList)
            //{
            //    TimeSpan deltaTime = DateTime.UtcNow - seat.ReservedTime;
            //    if (deltaTime.TotalMinutes > 1)
            //    {
            //        seat.isReserved = false;
            //        seat.ReservedTime = new DateTime(0001, 01, 01, 0, 0, 0);
                    
            //        Console.WriteLine($"Reservation for seat {seat.Row}{seat.Position} in {seat.Show.Description}({seat.Show.Event.Name}) has been released.");
            //    }
            //    await _context.SaveChangesAsync();
            //}
        }

    }
}
