
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data.DatabaseContext;

namespace TakeASeat.BackgroundServices
{
    public class ReleaseReservation : BackgroundService
    {
        private readonly DatabaseContext _context;
        public ReleaseReservation(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        private const int generalDelay = 2 * 60000; // 2 minutes
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
            var seatsList = await _context.Seats
                            .Where(s => s.Show.Date > DateTime.UtcNow
                            && s.isReserved.Equals(true)
                            && s.isSold.Equals(false))
                            .ToListAsync();

            foreach (var seat in seatsList)
            {
                TimeSpan deltaTime = DateTime.UtcNow - seat.ReservedTime;
                if (deltaTime.TotalMinutes > 5)
                {
                    seat.isReserved = false;
                    seat.ReservedTime = new DateTime(0001, 01, 01, 0, 0, 0);
                    _context.SaveChanges();
                    Console.WriteLine($"Reservation for seat {seat.Row}{seat.Position} in {seat.Show.Description}({seat.Show.Event.Name}) has been released.");
                }
            }
        }

    }
}
