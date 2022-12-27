using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class SeatReservation
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int? EventId { get; set; }
        public Event? Event { get; set; }

        [ForeignKey(nameof(Seat))]
        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
