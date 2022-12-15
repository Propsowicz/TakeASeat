using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }   
        public string Description { get; set; }
        public string ImageUri { get; set; }

        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public virtual IList<EventTagEventM2M> EventTags { get; set; }

        // M2M to Event TAG
        // public ICollection<EventTag> EventTypes { get; set; }        
    }
}
