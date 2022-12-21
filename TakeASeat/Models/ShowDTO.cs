namespace TakeASeat.Models
{
    public class CreateShowDTO
    {
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class GetShowDTO : CreateShowDTO
    {
        public int Id { get; set; }       
    }

    public class GetShowDetailsDTO : CreateShowDTO 
    {
        public int Id { get; set; }

        public List<GetSeatDTO> Seats { get; set; }
        public GetEventDTO Event { get; set; }
    }

    public class GetClosestShows : GetShowDTO
    {
        public GetEventDetailsToShowDTO Event { get; set; }

    }
}
