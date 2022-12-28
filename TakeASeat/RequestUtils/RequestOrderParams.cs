using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.RequestUtils
{
    public class RequestOrderParams
    {
        public string UserId { get; set; }
        //public int EventId { get; set; }
        public IEnumerable<ReserveSeatsDTO> Seats { get; set; }

    }
}
