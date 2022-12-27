using System.ComponentModel.DataAnnotations.Schema;

namespace TakeASeat.Data
{
    public class Ticket
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int SeatReservationId { get; set; }
        public SeatReservation SeatReservation { get; set; }

        
    }
}
