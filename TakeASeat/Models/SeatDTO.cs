using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Models
{
    public class CreateSeatDTO
    {
        [Required]
        public int ShowId { get; set; }
        [Required]
        public char Row { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Select position range between 4 and 20.")]
        public int Position { get; set; }
        [Required]        
        public double Price { get; set; }
        public string SeatColor { get; set; }

    }
    public class GetSeatDTO : CreateSeatDTO
    {
        public int Id { get; set; }
        public bool isReserved { get; set; } = false;
        public DateTime ReservedTime { get; set; }
        public bool isSold { get; set; } = false;
        public DateTime SoldTime { get; set; }
    }

    public class ReserveSeatsDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public char Row { get; set; }        
        public int Position { get; set; }
        public double Price { get; set; }
        //public bool isReserved { get; set; }
        //public DateTime ReservedTime { get; set; } = DateTime.UtcNow;
    }
}
