using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.TicketService;
using FluentAssertions;
using TakeASeat_Tests.UnitTests.Data;
using Microsoft.Extensions.Options;
using TakeASeat.ProgramConfigurations;
using FakeItEasy;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class TicketRepositoryTest
    {
        private readonly DatabaseContextMock _DbMock;
        private readonly IOptions<EmailProviderData> _emailData;
        public TicketRepositoryTest()
        {
            _DbMock= new DatabaseContextMock();
            _emailData = A.Fake<IOptions<EmailProviderData>>();
        }

        private async Task createSeatsForTests(DatabaseContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                await context.Seats.AddAsync(new Seat()
                {
                    Row = 'C',
                    Position = i + 1,
                    Price = 10,
                    SeatColor = "red",
                    ShowId = 3,
                });
            };
            for (int i = 0; i < 10; i++)
            {
                await context.Seats.AddAsync(new Seat()
                {
                    Row = 'D',
                    Position = i + 1,
                    Price = 100,
                    SeatColor = "red",
                    ShowId = 5,
                });
            };
            await context.SaveChangesAsync();
        }      

        [Fact]
        public async Task TicketRepository_CreateRangeOfTicketRecords_ShouldCreateTwoTickets()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            await createSeatsForTests(context);
            var repository = new TicketRepository(context, _emailData);
            var paymentTransaction = new PaymentTransaction()
            {
                Currency = "USD",
                Description = "SeatReservationsIds::1::"
            };
            await context.PaymentTransaction.AddAsync(paymentTransaction);
            await context.SaveChangesAsync();
            var seatReservation = new SeatReservation()
            {
                isReserved= true,
                ReservedTime= DateTime.UtcNow,
                PaymentTransactionId = paymentTransaction.Id,
                UserId = "testUserId"
            };
            await context.SeatReservation.AddAsync(seatReservation);
            await context.SaveChangesAsync();

            var seat_1 = context.Seats.FirstOrDefault(s => s.Id == 1);
            var seat_2 = context.Seats.FirstOrDefault(s => s.Id == 2);
            seat_1.ReservationId = seatReservation.Id;
            seat_2.ReservationId = seatReservation.Id;
            await context.SaveChangesAsync();

            // act
            var response = await repository.CreateRangeOfTicketRecords(paymentTransaction);

            // assert
            response.Should().HaveCount(2);
            response[0].Should().BeOfType(typeof(Ticket));
            response[0].PaymentTransactionId.Should().Be(paymentTransaction.Id);
            response[0].ShowId.Should().Be(3);
            response[1].ShowId.Should().Be(3);
        }
        [Fact]
        public async Task TicketRepository_CreateRangeOfTicketRecords_ShouldCreateFourTickets()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            await createSeatsForTests(context);
            var repository = new TicketRepository(context, _emailData);
            var paymentTransaction = new PaymentTransaction()
            {
                Currency = "USD",
                Description = "SeatReservationsIds::2::3::"
            };
            await context.PaymentTransaction.AddAsync(paymentTransaction);
            await context.SaveChangesAsync();

            var seatReservation_1 = new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                PaymentTransactionId = paymentTransaction.Id,
                UserId = "testUserId"
            };
            await context.SeatReservation.AddAsync(seatReservation_1);
            var seatReservation_2 = new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                PaymentTransactionId = paymentTransaction.Id,
                UserId = "testUserId"
            };
            await context.SeatReservation.AddAsync(seatReservation_2);
            await context.SaveChangesAsync();

            var seat_1 = context.Seats.FirstOrDefault(s => s.Id == 8);
            var seat_2 = context.Seats.FirstOrDefault(s => s.Id == 9);
            var seat_3 = context.Seats.FirstOrDefault(s => s.Id == 15);
            var seat_4 = context.Seats.FirstOrDefault(s => s.Id == 16);
            seat_1.ReservationId = seatReservation_1.Id;
            seat_2.ReservationId = seatReservation_1.Id;
            seat_3.ReservationId = seatReservation_2.Id;
            seat_4.ReservationId = seatReservation_2.Id;
            await context.SaveChangesAsync();

            // act
            var response = await repository.CreateRangeOfTicketRecords(paymentTransaction);

            // assert
            response.Should().HaveCount(4);
            response[0].Should().BeOfType(typeof(Ticket));
            response[0].PaymentTransactionId.Should().Be(paymentTransaction.Id);
            response[0].ShowId.Should().Be(3);
            response[1].ShowId.Should().Be(3);
            response[2].ShowId.Should().Be(5);
            response[3].ShowId.Should().Be(5);
        }
    }
}
