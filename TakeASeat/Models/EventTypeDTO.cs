using System.ComponentModel.DataAnnotations;

namespace TakeASeat.Models
{
    public class CreateEventTypeDTO
    {       
        [Required]
        [MaxLength(50, ErrorMessage = "Type is too long. Maximum length is 50 characters.")]
        [MinLength(2, ErrorMessage = "Type is too short. Minimum length is 2 characters.")]
        public string Name { get; set; }
    }
    public class GetEventTypeDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Type is too long. Maximum length is 50 characters.")]
        [MinLength(2, ErrorMessage = "Type is too short. Minimum length is 2 characters.")]
        public string Name { get; set; }
    }
}
