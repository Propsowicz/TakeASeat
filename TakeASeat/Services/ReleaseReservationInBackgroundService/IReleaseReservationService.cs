namespace TakeASeat.Services.BackgroundService
{
    public interface IReleaseReservationService
    {
        Task<string> ReleaseUnpaidReservations();
    }
}
