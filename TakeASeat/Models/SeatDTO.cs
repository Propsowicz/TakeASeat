using System.ComponentModel.DataAnnotations;

namespace TakeASeat.Models
{
    public class CreateSeatDTO
    {
        [Required]
        public char Row { get; set; }
        [Required]
        [Range(4, 40, ErrorMessage = "Select position range between 4 and 40.")]
        public int Position { get; set; }
        [Required]
        [Range(0, 2000, ErrorMessage = "Single ticket should cost between 0$ and 2000$.")]
        public double Price { get; set; }
    }
    public class GetSeatDTO : CreateSeatDTO
    {
        public int Id { get; set; }
    }
}
