using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services._Utils;
using TakeASeat.Services.TicketService;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _context;
        private readonly ITicketRepository _ticketRepository;

        public PaymentRepository(DatabaseContext context, ITicketRepository ticketRepository)
        {
            _context= context;
            _ticketRepository= ticketRepository;
        }
        public async Task createPaymentTransactionRecord(PaymentTransaction paymentTranscation)
        {
            await _context.PaymentTransaction.AddAsync(paymentTranscation);
            await _context.SaveChangesAsync();
            await addSeatReservationsToPaymentTransactionRecord(paymentTranscation.Id, paymentTranscation.Description);

            // START
            // Below Code is for a developer purpose only
            // It is a simulation of getting a response from the dotpay server
            // In production it should be exeucted as separate API endpoint
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
            await finishPaymentTransaction(mockResponseFromPaymentTransaction);
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

        public async Task finishPaymentTransaction(ResponseFromPaymentTransaction paymentResponse)
        {
            var dotpay_PIN = await _context.ProtectedKeys
                        .FirstOrDefaultAsync(k => k.Key == "DOTPAY_PIN");

            // Mock signature - for developer purpose only
            var signatureMockCreator = new PaymentServerResponse(dotpay_PIN.Value, paymentResponse).createResponseSignature();
            paymentResponse.signature = signatureMockCreator;

            PaymentServerResponse paymentServerResponse = new PaymentServerResponse(dotpay_PIN.Value, paymentResponse);
            if (paymentServerResponse.isValid())
            {
                List<int> listOfPaidSeatsReservationsIds = PaymentDescriptionToListOfReservationsConverter.Convert(paymentResponse.description);
                await setSeatReservationsIsSoldAsTrue(listOfPaidSeatsReservationsIds);

                PaymentTransaction paymentTransaction = await getPaymentTransactionObject(listOfPaidSeatsReservationsIds);
                await setPaymentTransactionIsAcceptedAsTrue(paymentTransaction);

                await _ticketRepository.CreateRangeOfTicketRecords(paymentTransaction);
                UserDataToSendEmailDTO userData = await getUserData(listOfPaidSeatsReservationsIds);
                await sendTicketsToUser(paymentTransaction, userData);
            }
        }
        private async Task setSeatReservationsIsSoldAsTrue(List<int> listOfPaidSeatsReservationsIds)
        {            
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                $"UPDATE SeatReservation SET isSold = 1 WHERE {RawSqlHelper.WHERE_Id_is_Id(listOfPaidSeatsReservationsIds)}"
                );
            await _context.Database.CommitTransactionAsync();
        }
        private async Task setPaymentTransactionIsAcceptedAsTrue(PaymentTransaction paymentTransaction)
        {            
            paymentTransaction.isAccepted = true;
            paymentTransaction.TransactionAcceptanceDateTime= DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        private async Task<PaymentTransaction> getPaymentTransactionObject(List<int> listOfPaidSeatsReservationsIds)
        {
            var paymentTransactionId = _context.SeatReservation.FirstOrDefault(sr => sr.Id == listOfPaidSeatsReservationsIds[0]).PaymentTransactionId;
            var paymentTransaction = await _context.PaymentTransaction.FirstOrDefaultAsync(pt => pt.Id == paymentTransactionId);
            ArgumentNullException.ThrowIfNull(paymentTransaction);
            return paymentTransaction;
        }
        private async Task<UserDataToSendEmailDTO> getUserData(List<int> listOfPaidSeatsReservationsIds) {
            var query = await _context.SeatReservation.Where(sr => sr.Id == listOfPaidSeatsReservationsIds[0]).Include(sr => sr.User).FirstOrDefaultAsync();
            return new UserDataToSendEmailDTO()
            {
                UserName = query.User.UserName, 
                Email = query.User.Email,
                FirstName= query.User.FirstName,
                LastName= query.User.LastName,
            };
        }
        private async Task sendTicketsToUser(PaymentTransaction paymentTransaction, UserDataToSendEmailDTO userData)
        {
            var listOfTickets = await _context.Ticket
                                            .AsNoTracking()
                                            .Where(t => t.PaymentTransactionId == paymentTransaction.Id)
                                            .Include(t => t.Show)
                                            .ToListAsync();
            await _ticketRepository.SendTicketsViaEmail(listOfTickets, userData);
        }
                
    }
}
