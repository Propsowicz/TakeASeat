using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services._Utils;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _context;

        public PaymentRepository(DatabaseContext context)
        {
            _context= context;
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

            ArgumentNullException.ThrowIfNull(mainQuery);

            var reservationsQuery = _context.SeatReservation
                        .Where(s => s.isReserved == true
                        && s.isSold == false)
                        .ToList();
            
            var dotpay_PIN = await _context.ProtectedKeys
                        .FirstOrDefaultAsync(k => k.Key == "DOTPAY_PIN");

            var dotpay_ID = await _context.ProtectedKeys
                        .FirstOrDefaultAsync(k => k.Key == "DOTPAY_ID");

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

        public async Task setPaymentTransactionRecordIsAcceptedToTrue(ResponseFromPaymentTransaction paymentResponse)
        {
            var dotpay_PIN = await _context.ProtectedKeys
                        .FirstOrDefaultAsync(k => k.Key == "DOTPAY_PIN");

            // START -- Mock signature - for developer purpose only
            var signatureMockCreator = new PaymentServerResponse(dotpay_PIN.Value, paymentResponse).createResponseSignature();
            paymentResponse.signature = signatureMockCreator;
            // END

            PaymentServerResponse paymentServerResponse = new PaymentServerResponse(dotpay_PIN.Value, paymentResponse);
            if (paymentServerResponse.PaymentValidation())
            {
                List<int> listOfPaidSeatsReservations = PaymentDescriptionToListOfReservationsConverter.Convert(paymentResponse.description);
                DateTime timeNow = DateTime.UtcNow;
                string sqlFormattedDate = timeNow.ToString("yyyy-MM-dd HH:mm:ss.fff");

                await _context.Database.BeginTransactionAsync();
                await _context.Database.ExecuteSqlRawAsync(
                    $"UPDATE SeatReservation SET isSold = True, SoldTime = {sqlFormattedDate} WHERE {RawSqlHelper.WHERE_Id_is_Id(listOfPaidSeatsReservations)}"
                    );                
                await _context.Database.CommitTransactionAsync();
                // need to create some kind of connection between payment data and transaction table
            }
        }
    }
}
