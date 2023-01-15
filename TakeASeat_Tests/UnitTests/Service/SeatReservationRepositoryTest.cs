using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatService;
using FakeItEasy;
using FluentAssertions;
using System.Data.Entity;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class SeatReservationRepositoryTest
    {
        private readonly ISeatRepository _seatRepository;
        public SeatReservationRepositoryTest()
        {
            _seatRepository = A.Fake<ISeatRepository>();
        }

        public async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDBSeatReservations")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;


            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();

            return contextMock;
        }
        public async Task<DatabaseContext> GetDatabaseContextIsolatedForSeatsTest()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDBSeats")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;


            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();

            return contextMock;
        }
        [Fact]
        public async Task SeatReservationRepository_DeleteEmptyReservation_ShouldDeleteReservation()
        {
            // arrange
            var context = await GetDatabaseContext();
            var repository = new SeatResRepository(context, _seatRepository);
            string userId = "123";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                UserId = userId,
                ReservedTime = DateTime.UtcNow,
            });
            await context.SaveChangesAsync();
            int seatReservationId = context.SeatReservation.FirstOrDefault(r => r.UserId == userId).Id;
            int oldNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();

            // act
            repository.DeleteEmptyReservation(seatReservationId);

            // assert
            int newNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();
            oldNumberOfReservationsByUser.Should().Be(1);
            newNumberOfReservationsByUser.Should().Be(0);
        }
        [Fact]
        public async Task SeatReservationRepository_DeleteEmptyReservation_ShouldNotDeleteReservation()
        {
            // arrange
            var context = await GetDatabaseContext();
            var repository = new SeatResRepository(context, _seatRepository);
            string userId = "1234";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                UserId = userId,
                ReservedTime = DateTime.UtcNow,
            });
            await context.SaveChangesAsync();
            int seatReservationId = context.SeatReservation.FirstOrDefault(r => r.UserId == userId).Id;
            await context.Seats.AddAsync(new Seat()
            {
                Row = 'B',
                Position = 2,
                Price = 15.4,
                SeatColor = "yellow",
                ShowId = 11,
                ReservationId = seatReservationId
            });
            await context.SaveChangesAsync();
            int oldNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();

            // act
            repository.DeleteEmptyReservation(seatReservationId);

            // assert
            int newNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();
            oldNumberOfReservationsByUser.Should().Be(1);
            newNumberOfReservationsByUser.Should().Be(1);
        }
        [Fact]
        public async Task SeatReservationRepository_RemoveSingleSeatFromOrder_ShouldDeleteOnlyOneSeat()
        {
            // arrange
            var context = await GetDatabaseContext();
            var repository = new SeatResRepository(context, _seatRepository);
            string userId = "12345";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                UserId = userId,
                ReservedTime = DateTime.UtcNow,
            });
            await context.SaveChangesAsync();
            int seatReservationId = context.SeatReservation.FirstOrDefault(r => r.UserId == userId).Id;
            IEnumerable<Seat> seats = new List<Seat>() {
                new Seat()
                {
                    Row = 'B',
                    Position = 1,
                    Price = 15.4,
                    SeatColor = "yellow",
                    ShowId = 11,
                    ReservationId = seatReservationId
                }, new Seat()
                {
                    Row = 'B',
                    Position = 2,
                    Price = 15.4,
                    SeatColor = "yellow",
                    ShowId = 11,
                    ReservationId = seatReservationId
                }
            };
            await context.Seats.AddRangeAsync(seats);
            await context.SaveChangesAsync();
            int oldNumberOfSeatsInReservation = context.Seats.Where(s => s.ReservationId == seatReservationId).Count();
            int oldNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();

            // act
            await repository.RemoveSingleSeatFromOrder(seatReservationId);

            // assert
            int newNumberOfSeatsInReservation = context.Seats.Where(s => s.ReservationId == seatReservationId).Count();
            int newNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();
            oldNumberOfReservationsByUser.Should().Be(1);
            newNumberOfReservationsByUser.Should().Be(1);
            oldNumberOfSeatsInReservation.Should().Be(2);
            newNumberOfSeatsInReservation.Should().Be(1);
        }
        [Fact]
        public async Task SeatReservationRepository_RemoveSingleSeatFromOrder_ShouldDeleteSeatAndReservation()
        {
            // arrange
            var context = await GetDatabaseContextIsolatedForSeatsTest();
            var repository = new SeatResRepository(context, _seatRepository);
            string userId = "123456abc";
            await context.SeatReservation.AddAsync(new SeatReservation()
            {
                isReserved = true,
                UserId = userId,
                ReservedTime = DateTime.UtcNow,
            });
            await context.SaveChangesAsync();
            int seatReservationId = context.SeatReservation.FirstOrDefault(r => r.UserId == userId).Id;
            IEnumerable<Seat> seats = new List<Seat>() {
                new Seat()
                {
                    Row = 'B',
                    Position = 1,
                    Price = 15.4,
                    SeatColor = "yellow",
                    ShowId = 11,
                    ReservationId = seatReservationId
                }

            };
            await context.Seats.AddRangeAsync(seats);
            await context.SaveChangesAsync();
            int oldNumberOfSeatsInReservation = context.Seats.Where(s => s.ReservationId == seatReservationId).Count();
            int oldNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();

            // act
            await repository.RemoveSingleSeatFromOrder(seatReservationId);

            // assert
            int newNumberOfSeatsInReservation = context.Seats.Where(s => s.ReservationId == seatReservationId).Count();
            int newNumberOfReservationsByUser = context.SeatReservation.Where(r => r.UserId == userId).ToList().Count();
            oldNumberOfReservationsByUser.Should().Be(1);
            newNumberOfReservationsByUser.Should().Be(0);
            oldNumberOfSeatsInReservation.Should().Be(1);
            newNumberOfSeatsInReservation.Should().Be(0);
        }

    }
}
