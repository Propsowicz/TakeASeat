using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // O2M to Event
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
