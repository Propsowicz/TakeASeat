using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Models;
//using System.Data.Entity;

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
        public async Task createPaymentTransaction(IEnumerable<SeatReservation> seatReservations, string userId)
        {
            throw new NotImplementedException();

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

            //b39582b4ac3c92451f21e25502bededa4bf9f89a1906c58edaffb5431491db93

            var PaymentData = new PaymentUtils(dotpay_PIN.Value, dotpay_ID.Value, mainQuery, reservationsQuery); ;                     
            return PaymentData.getPaymentData();
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

        
    }
}
