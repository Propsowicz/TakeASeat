using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }   
        public string Description { get; set; }
        public string ImageUri { get; set; }


        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public virtual IList<EventTagEventM2M> EventTags { get; set; }
        public virtual IList<Show> Shows { get; set; }
                
    }
}
