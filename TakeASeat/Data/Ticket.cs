using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Ticket
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int? EventId { get; set; }
        public Event? Event { get; set; }

        [ForeignKey(nameof(Seat))]        
        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }

        [ForeignKey(nameof(Buyer))]
        public int BuyerId { get; set; }
        public User Buyer { get; set; }
    }



}
