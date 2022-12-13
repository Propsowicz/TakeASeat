using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Seat
    {
        public int Id { get; set; }
        public char Row { get; set; }
        public int Position { get; set; }
        public double Price { get; set; }


        // O2M to Event
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
