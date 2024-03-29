﻿using TakeASeat.Data;
using TakeASeat.Services.SeatService;
using FluentAssertions;
using TakeASeat_Tests.UnitTests.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class SeatRepositoryTest
    {
        private readonly DatabaseContextMock _DbMock;
        public SeatRepositoryTest()
        {
            _DbMock= new DatabaseContextMock();
        }
            

        [Fact]
        public async Task SeatRepository_CreateMultipleSeats_ShouldCreateTwentySeats()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            var repository = new SeatRepository(context);
            List<Seat> seats = new List<Seat>();
            int showId = 2;
            for (int i = 0; i < 20; i++)
            {
                seats.Add(new Seat()
                {
                    Row = 'C',
                    Position = i + 1,
                    Price = 10,
                    SeatColor = "red",
                    ShowId = showId,
                });
            }

            // act
            await repository.CreateMultipleSeats(seats);

            // assert
            int newNumberOfSeatsByShow = context.Seats.Where(s => s.ShowId == showId).ToList().Count();
            newNumberOfSeatsByShow.Should().Be(20);
        }
        [Fact]
        public async Task SeatRepository_CreateMultipleSeats_ShouldCreateZeroSeats()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            var repository = new SeatRepository(context);
            List<Seat> seats = new List<Seat>();
            int showId = 98;

            // act
            await repository.CreateMultipleSeats(seats);

            // assert
            int newNumberOfSeatsByShow = context.Seats.Where(s => s.ShowId == showId).ToList().Count();
            newNumberOfSeatsByShow.Should().Be(0);
        }

    }
}
