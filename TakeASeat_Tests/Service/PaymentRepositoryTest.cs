using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Data.DatabaseContext;
using TakeASeat_Tests.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.PaymentService;
using TakeASeat.Data;
using TakeASeat.Models;
using Microsoft.EntityFrameworkCore;
using TakeASeat.ProgramConfigurations.DTO;

namespace TakeASeat_Tests.Service
{
    [Collection("Sequential")]
    public class PaymentRepositoryTest
    {      
        public async Task<DatabaseContext> GetDatabaseContextWithoutKeys()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDBPaymentIsolateWithoutKeys")
                .Options;

            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();
            
            return contextMock;
        }
        public async Task<DatabaseContext> GetDatabaseContextWithKeys()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDBPaymentIsolateWithKeys")
                .Options;

            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();
            await contextMock.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_PIN", Value = "123QWE456ASD" });
            await contextMock.ProtectedKeys.AddAsync(new ProtectedKeys() { Key = "DOTPAY_ID", Value = "123456789" });
            await contextMock.SaveChangesAsync();

            return contextMock;
        }
                
        [Fact]
        public async Task PaymentRepository_getPaymentData_ReturnPaymentData()
        {
            // arrange
            var context = await GetDatabaseContextWithKeys();
            PaymentRepository repository = new PaymentRepository(context);
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb0";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                ReservedTime= DateTime.UtcNow,
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
            response.chk.Should().Be("41ede15e263c02d1f356217bb78db7597d30308a98bd2ad35e7ae51af37705c8");            
        }
        [Fact]
        public async Task PaymentRepository_getPaymentData_ReturnNoCantAccessKeysException()
        {
            // arrange
            var context = await GetDatabaseContextWithoutKeys();
            PaymentRepository repository = new PaymentRepository(context);
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
            var context = await GetDatabaseContextWithKeys();
            PaymentRepository repository = new PaymentRepository(context);
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb0";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                ReservedTime = DateTime.UtcNow,
                UserId = userId
            });
            await context.SaveChangesAsync();
            var reservation = await context.SeatReservation.LastOrDefaultAsync();
            await context.Seats.AddAsync(new Seat()
            {
                Row = 'B',
                Position = 3,
                Price = 10,
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
