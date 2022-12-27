namespace TakeASeat.Services.BackgroundService
{
    public interface IReleaseReservationService
    {
        Task ReleaseUnpaidReservations();

    }
}
