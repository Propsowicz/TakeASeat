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
            await addSeatReservationsToPaymentTransactionRecord(paymentTranscation.Id, paymentTranscation.Description);

            // START
            // Below Code is for a developer purpose only
            // It is a simulation of getting a response from the dotpay server
            // it should be exeucted as separate API endpoint
            ResponseFromPaymentTransaction mockResponseFromPaymentTransaction = new ResponseFromPaymentTransaction()
            {
                id = "123456",
                operation_number = "test-123",
                operation_type = "payment",
                operation_status = "completed",
                operation_amount = Convert.ToString(4.3 * paymentTranscation.Amount),
                operation_currency = "PLN", 
                operation_original_amount = Convert.ToString(paymentTranscation.Amount),
                operation_original_currency = paymentTranscation.Currency,
                operation_datetime = DateTime.UtcNow.ToString(),
                control = string.Empty,
                description= paymentTranscation.Description,
                email = "random@test.com",
                p_info = "TakeASeat inc",
                p_email = "takeASeat@takeASeat.com",
                channel = "1"
            };
            await setPaymentTransactionRecordIsAcceptedToTrue(mockResponseFromPaymentTransaction);
            // END
        }

        private async Task addSeatReservationsToPaymentTransactionRecord (int paymentTransactionId, string paymentTransactionDescription)
        {
            List<int> listOfPaidSeatsReservations = PaymentDescriptionToListOfReservationsConverter.Convert(paymentTransactionDescription);
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE SeatReservation SET PaymentTransactionId = {paymentTransactionId} WHERE {RawSqlHelper.WHERE_Id_is_Id(listOfPaidSeatsReservations)}"
                );
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<PaymentDataDTO> getPaymentData(string userId)
        {
            var mainQuery = _context.Seats
                        .AsNoTracking()
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
                        .AsNoTracking()
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
                            .Where(s => s.SeatReservation.UserId == userId
                            && s.SeatReservation.isSold == false)
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
                await setSeatReservationsIsSoldAsTrue(listOfPaidSeatsReservations);
                await setPaymentTransactionIsAcceptedAsTrue(listOfPaidSeatsReservations);
            }
        }
        private async Task setSeatReservationsIsSoldAsTrue(List<int> listOfPaidSeatsReservations)
        {            
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE SeatReservation SET isSold = 1 WHERE {RawSqlHelper.WHERE_Id_is_Id(listOfPaidSeatsReservations)}"
                );
            await _context.Database.CommitTransactionAsync();
        }
        private async Task setPaymentTransactionIsAcceptedAsTrue(List<int> listOfSeatReservations)
        {
            var paymentTransactionId = _context.SeatReservation.FirstOrDefault(sr => sr.Id == listOfSeatReservations[0]).PaymentTransactionId;
            var paymentTransaction = await _context.PaymentTransaction.FirstOrDefaultAsync(pt => pt.Id == paymentTransactionId);
            paymentTransaction.isAccepted = true;
            paymentTransaction.TransactionAcceptanceDateTime= DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
                
    }
}
