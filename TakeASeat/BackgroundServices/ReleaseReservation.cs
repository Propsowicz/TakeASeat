
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

        private const int generalDelay = 2 * 10000; // 2 minutes
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
            //await _reservationReleaseRepository.ReleaseUnpaidReservations();
        }

    }
}
