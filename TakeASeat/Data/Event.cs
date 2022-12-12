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

        // M2M to Event TAG
        // public ICollection<EventTag> EventTypes { get; set; }        
    }
}
