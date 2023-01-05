using System.ComponentModel.DataAnnotations;

namespace TakeASeat.Models
{
    public class CreateEventTagDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Tag name is too long. Maximum length is 50 characters.")]
        [MinLength(2, ErrorMessage = "Tag name is too short. Minimum length is 2 characters.")]
        public string TagName { get; set; }
    }
    public class GetEventTagDTO 
    {
        public int Id { get; set; }        
        public string TagName { get; set; }
    }
}
