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
        [MinLength(10, ErrorMessage = "Description is too short. Minimum length is 10 characters.")]
        public string Description { get; set; }
        [Required]        
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Place name is too long. Maximum length is 250 characters.")]
        [MinLength(2, ErrorMessage = "Place name is too short. Minimum length is 20 characters.")]
        public string Place { get; set; }
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public string CreatorId { get; set; }
    }

    public class CreateEventWithTagsDTO
    {
        public CreateEventDTO eventDTO { get; set; }
        public List<GetEventTagDTO> eventTagsDTO { get; set; }

    }
    public class GetEventDTO 
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public int Duration { get; set; }        
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Place { get; set; }
        public string EventSlug { get; set; }
    }

    public class EditEventDTO 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name is too long. Maximum length is 100 characters.")]
        [MinLength(10, ErrorMessage = "Name is too short. Minimum length is 10 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(30, 240, ErrorMessage = "Choose duration of event between 30 and 240 minutes.")]
        public int Duration { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description is too long. Maximum length is 250 characters.")]
        [MinLength(10, ErrorMessage = "Description is too short. Minimum length is 10 characters.")]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Place name is too long. Maximum length is 250 characters.")]
        [MinLength(2, ErrorMessage = "Place name is too short. Minimum length is 20 characters.")]
        public string Place { get; set; }
        [Required]
        public int EventTypeId { get; set; }        
        
    }


    public class EditEventWithTagsDTO
    {
        public EditEventDTO eventDTO { get; set; }
        public List<GetEventTagDTO> eventTagsDTO { get; set; }

    }

    public class GetEventWithListOfShowsDTO : GetEventDTO
    {
        public IList<GetShowDTO> Shows { get; set; }

    }

    public class GetEventDetailsDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Place { get; set; }
        public string EventSlug { get; set; }
        public IList<GetEventTagEventM2MDTO> EventTags { get; set; }
        public GetEventTypeDTO EventType { get; set; }
        public GetUserDTO Creator { get; set; }
        public IList<GetShowDTO> Shows { get; set; }
    }

    public class GetEventDetailsToShowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Place { get; set; }
        public string EventSlug { get; set; }
        public GetUserDTO Creator { get; set; }
        public CreateEventTypeDTO EventType { get; set; }
        public IList<GetEventTagEventM2MDTO> EventTags { get; set; }
    }

}
