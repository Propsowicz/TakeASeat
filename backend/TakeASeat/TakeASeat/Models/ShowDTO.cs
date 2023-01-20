using System.ComponentModel.DataAnnotations;
using TakeASeat.Models.CustomValidators;

namespace TakeASeat.Models
{
    public class CreateShowDTO
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        [DateFiveDaysGraterThanToday]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description is too long. Maximum length is 250 characters.")]
        [MinLength(10, ErrorMessage = "Description is too short. Minimum length is 10 characters.")]
        public string Description { get; set; }
    }

    public class GetShowDTO 
    {
        public int Id { get; set; }   
        public bool IsReadyToSell { get; set; }
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class GetShowDetailsDTO  
    {
        public int EventId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public GetEventDetailsToShowDTO Event { get; set; }
    }

    public class GetClosestShow : GetShowDTO
    {        
        public GetEventDetailsToShowDTO Event { get; set; }

    }

    public class GetShowsByEventDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }        
        public string EventSlug { get; set; }        
        public int SeatsLeft { get; set; }
        public bool IsReadyToSell { get; set; }

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


