using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class SeatReservation
    {
        public int Id { get; set; }

        public bool isReserved { get; set; } = false;
        public DateTime ReservedTime { get; set; }
        public bool isSold { get; set; } = false;
        public DateTime SoldTime { get; set; }


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        public virtual IList<Seat> Seats { get; set; }
    }
}
