using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Seat
    {
        public int Id { get; set; }
        public char Row { get; set; }
        public int Position { get; set; }
        public double Price { get; set; }
        public bool isTaken { get; set; } = false;
        public bool isSold { get; set; } = false;


        [ForeignKey(nameof(Show))]
        public int ShowId { get; set; }
        public Show Show { get; set; }

    }
}
