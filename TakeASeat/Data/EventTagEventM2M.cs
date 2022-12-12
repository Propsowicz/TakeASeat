using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class EventTagEventM2M
    {
        public int Id { get; set; }

        // O2M Event
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; }

        // O2M EventTag
        [ForeignKey(nameof(EventTag))]
        public int EventTagId { get; set; }
        public EventTag EventTag { get; set; }
    }
}
