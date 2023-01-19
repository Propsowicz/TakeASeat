using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services._Utils;
using FluentAssertions;
using TakeASeat.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class UtilsTest
    {
        [Fact]
        public void PaymentDescriptionToListOfReservationsConverter_Convert_ReturnListWithTwoIntegers()
        {
            // arrange
            var testString = "SeatReservationsIds:{2,3,}";

            // act
            var response = PaymentDescriptionToListOfReservationsConverter.Convert(testString);

            // assert
            response.Should().HaveCount(2);
            response[0].Should().Be(2);
            response[1].Should().Be(3);
        }
        [Fact]
        public void PaymentDescriptionToListOfReservationsConverter_Convert_ReturnEmptyList()
        {
            // arrange
            var testString = "SeatReservationsIds:{}";

            // act
            var response = PaymentDescriptionToListOfReservationsConverter.Convert(testString);

            // assert
            response.Should().HaveCount(0);            
        }

        [Fact]
        public void RawSqlHelper_WHERE_ReservationId_is_Id_OneId ()
        {
            // arrange
            List<int> listOfIds = new List<int> { 1 };

            // act
            var sqlString = RawSqlHelper.WHERE_ReservationId_is_Id(listOfIds);

            // assert
            sqlString.Should().Be("ReservationId = 1");
        }

        [Fact]
        public void RawSqlHelper_WHERE_ReservationId_is_Id_ThreeIds()
        {
            // arrange
            List<int> listOfIds = new List<int> { 1, 5 , 7 };

            // act
            var sqlString = RawSqlHelper.WHERE_ReservationId_is_Id(listOfIds);

            // assert
            sqlString.Should().Be("ReservationId = 1 OR ReservationId = 5 OR ReservationId = 7");
        }

        [Fact]
        public void RawSqlHelper_WHERE_Id_is_SeatId_OneId()
        {
            // arrange
            List<Seat> listOfSeats = new List<Seat>()
            {
                new Seat () { Id= 1 },
            };
            IEnumerable<Seat> enuList = listOfSeats;

            // act
            var sqlString = RawSqlHelper.WHERE_Id_is_SeatId(enuList);

            // assert
            sqlString.Should().Be("Id = 1");
        }
        [Fact]
        public void RawSqlHelper_WHERE_Id_is_SeatId_ThreeIds()
        {
            // arrange
            List<Seat> listOfSeats = new List<Seat>()
            {
                new Seat () { Id= 1 },
                new Seat () { Id= 11 },
                new Seat () { Id= 111 },
            };
            IEnumerable<Seat> enuList = listOfSeats;

            // act
            var sqlString = RawSqlHelper.WHERE_Id_is_SeatId(enuList);

            // assert
            sqlString.Should().Be("Id = 1 OR Id = 11 OR Id = 111");
        }

        [Fact]
        public void RawSqlHelper_WHERE_Id_is_Id_OneId()
        {
            // arrange
            List<int> listOfIds = new List<int> { 1 };

            // act
            var sqlString = RawSqlHelper.WHERE_Id_is_Id(listOfIds);

            // assert
            sqlString.Should().Be("Id = 1");
        }

        [Fact]
        public void RawSqlHelper_WHERE_Id_is_Id_ThreeIds()
        {
            // arrange
            List<int> listOfIds = new List<int> { 1, 5, 7 };

            // act
            var sqlString = RawSqlHelper.WHERE_Id_is_Id(listOfIds);

            // assert
            sqlString.Should().Be("Id = 1 OR Id = 5 OR Id = 7");
        }
    }
}
