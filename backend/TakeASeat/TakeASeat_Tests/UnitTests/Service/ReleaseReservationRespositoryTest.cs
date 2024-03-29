﻿using TakeASeat.Services.SeatReservationService;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Data;
using TakeASeat.Services.BackgroundService;
using TakeASeat_Tests.UnitTests.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class ReleaseReservationRespositoryTest
    {
        private readonly ISeatResRepository _seatReservationRepository;
        private readonly DatabaseContextMock _DbMock;
        private readonly DateTime _timeNow;

        public ReleaseReservationRespositoryTest()
        {
            _seatReservationRepository = A.Fake<ISeatResRepository>();
            _DbMock = new DatabaseContextMock();
            _timeNow = DateTime.UtcNow;
        }
        

        public List<SeatReservation> createMockSeatReservations(DateTime seatReservationNumberOne, DateTime seatReservationNumberTwo)

        {
            List<SeatReservation> seatResevations = new List<SeatReservation>()
            {
                new SeatReservation()
                {
                    isReserved = true,
                    ReservedTime= seatReservationNumberOne,
                    isSold = false,
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                },
                new SeatReservation()
                {
                    isReserved = true,
                    ReservedTime= seatReservationNumberTwo,
                    isSold = false,
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                }
            };
            return seatResevations;
        }

        [Fact]
        public async Task ReleaseReservationService_ReleaseUnpaidReservations_ShouldNotDeleteAnyReservation()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            DateTime seatReservationNumberOne = _timeNow.AddMinutes(-1);
            DateTime seatReservationNumberTwo = _timeNow.AddMinutes(-1);
            var seatResevations = createMockSeatReservations(seatReservationNumberOne, seatReservationNumberTwo);

            ReleaseReservationService repository = new ReleaseReservationService(context, _seatReservationRepository);
            await context.SeatReservation.AddRangeAsync(seatResevations);
            await context.SaveChangesAsync();

            // act
            var response = await repository.ReleaseUnpaidReservations();

            // assert            
            response.Should().Be("No unpaid reservations has been found...");
        }
        [Fact]
        public async Task ReleaseReservationService_ReleaseUnpaidReservations_ShouldDeleteReservation_1()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            DateTime seatReservationNumberOne = _timeNow.AddMinutes(-4);
            DateTime seatReservationNumberTwo = _timeNow.AddMinutes(-6);
            var seatResevations = createMockSeatReservations(seatReservationNumberOne, seatReservationNumberTwo);

            ReleaseReservationService repository2 = new ReleaseReservationService(context, _seatReservationRepository);
            await context.SeatReservation.AddRangeAsync(seatResevations);
            await context.SaveChangesAsync();

            // act
            var response = await repository2.ReleaseUnpaidReservations();

            // assert
            response.Should().Be("Unpaid reservations has been deleted...");
        }
        [Fact]
        public async Task ReleaseReservationService_ReleaseUnpaidReservations_ShouldDeleteReservation_2()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            DateTime seatReservationNumberOne = _timeNow.AddMinutes(-4);
            DateTime seatReservationNumberTwo = _timeNow.AddMinutes(-55);
            var seatResevations = createMockSeatReservations(seatReservationNumberOne, seatReservationNumberTwo);

            await context.SeatReservation.AddRangeAsync(seatResevations);
            await context.SaveChangesAsync();
            ReleaseReservationService repository = new ReleaseReservationService(context, _seatReservationRepository);

            // act
            var response = await repository.ReleaseUnpaidReservations();

            // assert
            response.Should().Be("Unpaid reservations has been deleted...");
        }
        [Fact]
        public async Task ReleaseReservationService_ReleaseUnpaidReservations_ShouldDeleteReservation_3()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            DateTime seatReservationNumberOne = _timeNow.AddMinutes(-4);
            DateTime seatReservationNumberTwo = _timeNow.AddHours(-1);
            var seatResevations = createMockSeatReservations(seatReservationNumberOne, seatReservationNumberTwo);

            await context.SeatReservation.AddRangeAsync(seatResevations);
            await context.SaveChangesAsync();
            ReleaseReservationService repository = new ReleaseReservationService(context, _seatReservationRepository);

            // act
            var response = await repository.ReleaseUnpaidReservations();

            // assert
            response.Should().Be("Unpaid reservations has been deleted...");
        }

    }
}
