using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services._Utils;
using FluentAssertions;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class UtilsTest
    {
        [Fact]
        public void PaymentDescriptionToListOfReservationsConverter_Convert_ReturnListWithTwoIntegers()
        {
            // arrange
            var testString = "SeatReservationsIds::2::3::";

            // act
            var response = PaymentDescriptionToListOfReservationsConverter.Convert(testString);

            // assert
            response.Should().HaveCount(2);
            response[0].Should().Be(2);
            response[1].Should().Be(3);
        }

    }
}
