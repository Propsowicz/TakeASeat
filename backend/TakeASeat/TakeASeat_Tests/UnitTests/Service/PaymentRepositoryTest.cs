using TakeASeat_Tests.UnitTests.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.PaymentService;
using TakeASeat.Data;
using TakeASeat.Models;
using Microsoft.EntityFrameworkCore;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.Services.TicketService;
using Microsoft.Extensions.Options;
using TakeASeat.ProgramConfigurations;

namespace TakeASeat_Tests.UnitTests.Service
{
    [Collection("Sequential")]
    public class PaymentRepositoryTest
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly DatabaseContextMock _DbMock;
        private readonly IOptions<PaymentServerData> _keysData;
        public PaymentRepositoryTest()
        {
            _ticketRepository = A.Fake<ITicketRepository>();
            _DbMock = new DatabaseContextMock();
            _keysData = A.Fake<IOptions<PaymentServerData>>();
        }        
        
        [Fact]
        public async Task PaymentRepository_getPaymentData_ReturnNoCantAccessKeysException()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            PaymentRepository repository = new PaymentRepository(context, _ticketRepository, _keysData);
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

            PaymentRepository repository = new PaymentRepository(context, _ticketRepository, _keysData);
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
