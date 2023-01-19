using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsReadyToSell { get; set; } = false;

        // O2M to Event
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; }

        public virtual IList<Seat> Seats { get; set; }
    }
}
