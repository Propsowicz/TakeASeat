using TakeASeat.Services.BackgroundService;

namespace TakeASeat.BackgroundServices
{
    public class ReleaseReservation : BackgroundService
    {

        private readonly IReleaseReservationService _reservationReleaseRepository;
        public ReleaseReservation(IServiceProvider serviceProvider)
        {
            _reservationReleaseRepository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IReleaseReservationService>();
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
            Console.WriteLine("Cleaning up unpaid reservations..");
            var result = await _reservationReleaseRepository.ReleaseUnpaidReservations();
            Console.WriteLine(result);
            // above code need to be uncommnet in production -> comment only for debugging
        }

    }
}
