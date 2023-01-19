namespace TakeASeat.RequestUtils
{
    public class RequestPaymentParams
    {
        public string? UserId { get; set; }
    }

    public class RequestPaymentDeleteParams
    {
        public int SeatReservationId { get; set; }
    }
}
