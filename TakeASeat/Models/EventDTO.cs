using System.ComponentModel.DataAnnotations;
using TakeASeat.Data;

namespace TakeASeat.Models
{
    public class CreateEventDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name is too long. Maximum length is 100 characters.")]
        [MinLength(10, ErrorMessage = "Name is too short. Minimum length is 10 characters.")]
        public string Name { get; set; }       
        [Required]
        [Range(30, 240, ErrorMessage = "Choose duration of event between 30 and 240 minutes.")]
        public int Duration { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description is too long. Maximum length is 250 characters.")]
        [MinLength(20, ErrorMessage = "Description is too short. Minimum length is 20 characters.")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Place { get; set; }
        public string EventSlug { get; set; }
    }
    public class GetEventDTO : CreateEventDTO
    {
        public int Id { get; set; }       

    }

    public class GetEventDetailsDTO : GetEventDTO 
    {
        public IList<GetEventTagEventM2MDTO> EventTags { get; set; }
        public CreateEventTypeDTO EventType { get; set; }
        public GetUserDTO Creator { get; set; }
        public IList<GetShowDTO> Shows { get; set; }
    }

    public class GetEventDetailsToShowDTO : GetEventDTO
    {
        public GetUserDTO Creator { get; set; }
        public CreateEventTypeDTO EventType { get; set; }
        public IList<GetEventTagEventM2MDTO> EventTags { get; set; }
    }

}
