namespace TakeASeat.Models
{
    public class CreateEventTagEventM2MDTO
    {
        public int EventTagId { get; set; }
        public int EventId { get; set; }
    }

    public class GetEventTagEventM2MDTO : CreateEventTagEventM2MDTO
    {
        public int Id { get; set; }
    }

    
}
