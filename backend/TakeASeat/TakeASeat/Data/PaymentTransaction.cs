using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public bool isAccepted { get; set; } = false;
        public DateTime TransactionDateTime { get; set; } = DateTime.UtcNow;
        public DateTime TransactionAcceptanceDateTime { get; set; }

        public virtual IList<SeatReservation> SeatReservations { get; set; }
        
    }
}
