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
            var paymentData = new PaymentDataDTO();
            var mainQuery = _context.Seats
                        .Where(s => s.SeatReservation.UserId == userId
                        && s.SeatReservation.isReserved == true
                        && s.SeatReservation.isSold == false)
                        .Select(s => s.Price)
                        .ToList();

            var reservationsQuery = _context.SeatReservation                        
                        .Where(s => s.isReserved == true
                        && s.isSold == false)
                        .Include(r => r.User)
                        .ToList();

            paymentData.Amount = mainQuery.Sum();
            paymentData.Description = $"paymentForTicketsByUser{reservationsQuery[0].User.UserName}";
            paymentData.Id = "";
            foreach(var reservation in reservationsQuery)
            {
                paymentData.Id += reservation.Id + ";";
            }

            return paymentData;
        }

        public async Task<IList<SeatReservation>> getSeatReservations(string userId)
        {
            return await _context.SeatReservation
                        .Where(r => r.UserId == userId
                        && r.isReserved == true
                        && r.isSold == false)
                        .Include(r => r.Seats)
                        .Include(r => r.User)
                        .ToListAsync();
        }

        public async Task removeSeatReservations(int seatReservationId)
        {
            var seatReservation = await _context.SeatReservation
                        .Where(r => r.Id == seatReservationId)
                        .Include(r => r.Seats)
                        .ToListAsync();

            await _seatResRepository.DeleteSeatReservation(seatReservation);            
        }
    }
}
