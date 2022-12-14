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


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        
    }
}
