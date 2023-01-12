using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatService;
using FluentAssertions;

namespace TakeASeat_Tests.Service
{
    public class SeatRepositoryTest
    {
        public async Task<DatabaseContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MockDBSeats")
                .Options;

            var contextMock = new DatabaseContext(options);
            contextMock.Database.EnsureCreated();

            return contextMock;
        }

        [Fact]
        public async Task SeatRepository_CreateMultipleSeats_ShouldCreateTwentySeats()
        {
            // arrange
            var context = await GetDatabaseContext();
            var repository = new SeatRepository(context);
            List<Seat> seats = new List<Seat>();
            int showId = 99;
            for(int i = 0; i < 20; i++)
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
            var context = await GetDatabaseContext();
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
