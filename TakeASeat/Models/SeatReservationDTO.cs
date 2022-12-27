using System.ComponentModel.DataAnnotations.Schema;
using TakeASeat.Data;

namespace TakeASeat.Models
{
    public class SeatReservationDTO
    {
        public int EventId { get; set; }
        public int SeatId { get; set; }
        public string UserId { get; set; }
    }
}
