using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat.Services.PaymentService
{
    public interface IPaymentRepository
    {

        Task<IList<SeatReservation>> getSeatReservations(string userId);
        Task removeSeatReservations(int seatReservationId);
        Task<PaymentDataDTO> getPaymentData(string userId);
        //Task<IList<string>> getPaymentData(string userId);
        Task createPaymentTransaction(IEnumerable<SeatReservation> seatReservations, string userId);


    }
}
