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
        [MaxLength(50, ErrorMessage = "Type is too long. Maximum length is 50 characters.")]
        [MinLength(2, ErrorMessage = "Type is too short. Minimum length is 2 characters.")]
        public string Type { get; set; }
        [Required]
        [Range(30, 240, ErrorMessage = "Choose duration of event between 30 and 240 minutes.")]
        public int Duration { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description is too long. Maximum length is 250 characters.")]
        [MinLength(20, ErrorMessage = "Description is too short. Minimum length is 20 characters.")]
        public string Description { get; set; }
        public string ImageUri { get; set; }
    }
    public class GetEventDTO : CreateEventDTO
    {
        public int Id { get; set; }
        public IList<GetEventTagEventM2MDTO> EventTags { get; set; }

    }

    //public class GetEventWithTagsDTO : GetEventDTO
    //{
    //    public IList<CreateEventTagEventM2MDTO> EventTags { get; set; }
    //}
}
