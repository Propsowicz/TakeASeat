using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _context;
        private readonly ISeatResRepository _seatResRepository;

        public PaymentRepository(DatabaseContext context, ISeatResRepository seatResRepository)
        {
            _context= context;
            _seatResRepository= seatResRepository;
        }
        public async Task createPaymentTransactionRecord(PaymentTransaction paymentTranscation)
        {
            await _context.PaymentTransaction.AddAsync(paymentTranscation);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentDataDTO> getPaymentData(string userId)
        {

            var mainQuery = _context.Seats
                        .Where(s => s.SeatReservation.UserId == userId
                        && s.SeatReservation.isReserved == true
                        && s.SeatReservation.isSold == false)
                        .Select(s => s.Price)
                        .ToList();

            var reservationsQuery = _context.SeatReservation
                        .Where(s => s.isReserved == true
                        && s.isSold == false)
                        .ToList();

            var dotpay_PIN = _context.ProtectedKeys
                        .FirstOrDefault(k => k.Key == "DOTPAY_PIN");

            var dotpay_ID = _context.ProtectedKeys
                        .FirstOrDefault(k => k.Key == "DOTPAY_ID");

            if (dotpay_PIN != null && dotpay_ID != null) 
            {
                var paymentData = new PaymentData(dotpay_PIN.Value, dotpay_ID.Value, mainQuery, reservationsQuery);
                return paymentData.getPaymentData();
            }
            else
            {
                throw new CantAccessDataException("Can't access Payment Server Keys.");
            }            
        }

        public async Task<IList<Seat>> getReservedSeats(string userId)
        {
            return await _context.Seats
                        .Where(s => s.SeatReservation.UserId == userId
                        && s.SeatReservation.isReserved == true
                        && s.SeatReservation.isSold == false)
                        .Include(s => s.SeatReservation)
                        .Include(s => s.Show)
                            .ThenInclude(s => s.Event)
                        .ToListAsync();
        }

        public async Task<GetTotalCostByUser> getTotalCost(string userId)
        {
            var query = await _context.Seats
                            .AsNoTracking()
                            .Where(s => s.SeatReservation.UserId == userId)
                            .Select(s => s.Price)
                            .ToListAsync();
                            
            return new GetTotalCostByUser { TotalCost = Math.Round(query.Sum(), 2) };
        }
    }
}
