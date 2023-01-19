using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat.Services.PaymentService
{
    public interface IPaymentRepository
    {
        Task<IList<Seat>> getReservedSeats(string userId);
        Task<PaymentDataDTO> getPaymentData(string userId);
        Task<GetTotalCostByUser> getTotalCost(string userId);
        Task createPaymentTransactionRecord(PaymentTransaction paymentTranscation);
        Task finalizeTicketOrder(ResponseFromPaymentTransaction paymentResponse);
    }
}
