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
        public bool IsReadyToSell { get; set; }
    }

    public class GetShowDetailsDTO : CreateShowDTO 
    {
        public int Id { get; set; }

        //public List<GetSeatDTO> Seats { get; set; }
        public GetEventDetailsToShowDTO Event { get; set; }
    }

    public class GetClosestShows : GetShowDTO
    {
        private int _number = 0;
        public int Number {
            get { 
                return 0; 
            
            }
            
        }
        public GetEventDetailsToShowDTO Event { get; set; }

    }

    public class GetShowsByEventDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }        
        public string EventSlug { get; set; }        
        public int SeatsLeft { get; set; }
    }

    public class GetShowsDTO
    {
        public int Id { get; set; }
        public bool IsReadyToSell { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public string EventName { get; set; }
        public int EventId { get; set; }
        public string EventSlug { get; set; }
        public string EventPlace { get; set; }
        public string EventType { get; set; }
        public List<string> EventTags { get; set; }
        public int SeatsLeft { get; set; }

    }

}
