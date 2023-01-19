using TakeASeat_Tests.UnitTests.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.PaymentService;
using TakeASeat.Data;
using TakeASeat.Models;
using Microsoft.EntityFrameworkCore;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services.TicketService;

namespace TakeASeat_Tests.UnitTests.Service
{
    [Collection("Sequential")]
    public class PaymentRepositoryTest
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly DatabaseContextMock _DbMock;
        public PaymentRepositoryTest()
        {
            _ticketRepository = A.Fake<ITicketRepository>();
            _DbMock = new DatabaseContextMock();
        }        

        [Fact]
        public async Task PaymentRepository_getPaymentData_ReturnPaymentData()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            await context.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_PIN", Value = "123QWE456ASD" });
            await context.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_ID", Value = "123456789" });
            await context.SaveChangesAsync();

            PaymentRepository repository = new PaymentRepository(context, _ticketRepository);
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb0";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                UserId = userId
            });
            await context.SaveChangesAsync();
            var reservation = await context.SeatReservation.LastOrDefaultAsync();
            await context.Seats.AddRangeAsync(new Seat()
            {
                Row = 'B',
                Position = 1,
                Price = 15.4,
                SeatColor = "yellow",
                ShowId = 11,
                ReservationId = reservation.Id
            }, new Seat()
            {
                Row = 'B',
                Position = 2,
                Price = 15.4,
                SeatColor = "yellow",
                ShowId = 11,
                ReservationId = reservation.Id
            }
            );
            await context.SaveChangesAsync();

            // act
            var response = await repository.getPaymentData(userId);

            // assert            
            response.Should().BeOfType(typeof(PaymentDataDTO));
            response.amount.Should().Be("30,8");
            response.chk.Should().Be("ae0071da28ba768cf04ca420637cfdfa8b79f6884fc93f1cfbc7994acb41ebbf");
        }
        [Fact]
        public async Task PaymentRepository_getPaymentData_ReturnNoCantAccessKeysException()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            PaymentRepository repository = new PaymentRepository(context, _ticketRepository);
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                UserId = userId
            });
            await context.SaveChangesAsync();
            var reservation = await context.SeatReservation.LastOrDefaultAsync();
            await context.Seats.AddRangeAsync(new Seat()
            {
                Row = 'B',
                Position = 1,
                Price = 15.4,
                SeatColor = "yellow",
                ShowId = 11,
                ReservationId = reservation.Id
            }, new Seat()
            {
                Row = 'B',
                Position = 2,
                Price = 15.4,
                SeatColor = "yellow",
                ShowId = 11,
                ReservationId = reservation.Id
            }
            );
            await context.SaveChangesAsync();

            // act
            Func<Task> act = async () =>
            { await repository.getPaymentData(userId); };

            // assert            
            act.Should().ThrowExactlyAsync<CantAccessDataException>().WithMessage("Can't access Payment Server Keys.");
        }
        [Fact]
        public async Task PaymentRepository_getTotalCost_ReturnNumber40comma8()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            await context.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_PIN", Value = "123QWE456ASD" });
            await context.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_ID", Value = "123456789" });
            await context.SaveChangesAsync();

            PaymentRepository repository = new PaymentRepository(context, _ticketRepository);
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb0";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                UserId = userId
            });
            await context.SaveChangesAsync();
            var reservation = await context.SeatReservation.LastOrDefaultAsync();
            await context.Seats.AddRangeAsync(new Seat()
            {
                Row = 'B',
                Position = 3,
                Price = 10,
                SeatColor = "blue",
                ShowId = 11,
                ReservationId = reservation.Id
            },
            new Seat()
            {
                Row = 'B',
                Position = 4,
                Price = 30.8,
                SeatColor = "blue",
                ShowId = 11,
                ReservationId = reservation.Id
            }
            );
            await context.SaveChangesAsync();

            // act
            var response = await repository.getTotalCost(userId);

            // assert            
            response.Should().BeOfType(typeof(GetTotalCostByUser));
            response.TotalCost.Should().Be(40.8);


        }
    }
}
