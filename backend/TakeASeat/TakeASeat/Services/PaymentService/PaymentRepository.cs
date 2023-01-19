using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services._Utils;
using TakeASeat.Services.TicketService;
using TakeASeat.ProgramConfigurations;
using Microsoft.Extensions.Options;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _context;
        private readonly ITicketRepository _ticketRepository;
        private readonly PaymentServerData _paymentData;

        public PaymentRepository(DatabaseContext context, ITicketRepository ticketRepository, IOptions<PaymentServerData> options)
        {
            _context= context;
            _ticketRepository= ticketRepository;
            _paymentData = options.Value;
        }
        public async Task createPaymentTransactionRecord(PaymentTransaction paymentTranscation)
        {
            await _context.PaymentTransaction.AddAsync(paymentTranscation);
            await _context.SaveChangesAsync();
            await addSeatReservationsToPaymentTransactionRecord(paymentTranscation.Id, paymentTranscation.Description);

            // START
            // Below Code is for a developer purpose only
            // It is a simulation of getting a response from the dotpay server
            // In production it should be executed as separate API endpoint
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
            await finalizeTicketOrder(mockResponseFromPaymentTransaction);
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
                        
            var dotpay_PIN = _paymentData.PIN;
            var dotpay_ID = _paymentData.ID;

            if (dotpay_PIN != null && dotpay_ID != null) 
            {
                var paymentData = new PaymentData(dotpay_PIN, dotpay_ID, mainQuery, reservationsQuery);
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

        public async Task finalizeTicketOrder(ResponseFromPaymentTransaction paymentResponse)
        {            
            var dotpay_PIN = _paymentData.PIN;

            // Mock signature - for developer purpose only
            var signatureMockCreator = new PaymentServerResponse(dotpay_PIN, paymentResponse).createResponseSignature();
            paymentResponse.signature = signatureMockCreator;

            PaymentServerResponse paymentServerResponse = new PaymentServerResponse(dotpay_PIN, paymentResponse);
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
