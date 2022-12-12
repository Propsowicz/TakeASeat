namespace TakeASeat.Data
{
    public class EventTag
    {
        public int Id { get; set; }
        public string TagName { get; set; }

        // M2M to Event
        // public ICollection<Event> Events { get; set; }
    }
}
