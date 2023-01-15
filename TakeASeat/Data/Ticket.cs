using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TakeASeat.Data
{
    public class Ticket
    {
        // ticket data shoulb be unchangeable
        public int Id { get; set; }
        public int TickedCode { get; set; }
        public char Row { get; set; }
        public int Position { get; set; }
        public double Price { get; set; }
        public string ShowDescription { get; set; }
        public string EventName { get; set; }
        public DateTime ShowDate { get; set; }


        // data just to query tickets if needed
        [ForeignKey(nameof(Show))]
        public int ShowId { get; set; }
        public Show Show { get; set; }

        [ForeignKey(nameof(PaymentTransactionId))]
        public int PaymentTransactionId { get; set; }
        public PaymentTransaction PaymentTransaction { get; set; }
    }
}
