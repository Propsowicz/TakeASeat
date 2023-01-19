using TakeASeat.Data;
using FluentAssertions;
using TakeASeat.Services.PaymentService;
using TakeASeat.Models;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class PaymentDataTest
    {
        string _DOTPAY_PIN = "123QWE456ASD";
        string _DOTPAY_ID = "123456789";

        [Fact]
        public void PaymentData_getPaymentData_ReturnRightPaymentDataAmountWithoutDecimalPlaces()
        {
            // arrange
            List<double> listOfPrices = new List<double>() { 5.5, 5.5, 9, 10, 10, 10 };
            List<SeatReservation> seatReservations = new List<SeatReservation>
        {
            new SeatReservation
            {
                Id = 1,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 2,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 3,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 4,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 5,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 6,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            }
        };
            PaymentData paymentData = new PaymentData(_DOTPAY_PIN, _DOTPAY_ID, listOfPrices, seatReservations);

            // act
            var result = paymentData.getPaymentData();

            // assert
            result.chk.Should().Be("1240a881735f094cd9c0784998b5b7e31cfe6ac270c6ab8ddf59bb6e7249acbb");
            result.description.Should().Be("SeatReservationsIds:{1,2,3,4,5,6,}");
            result.amount.Should().Be("50");
            result.currency.Should().Be("USD");
        }
        [Fact]
        public void PaymentData_getPaymentData_ReturnRightPaymentDataAmountWithDecimalPlaces()
        {
            // arrange
            List<double> listOfPrices = new List<double>() { 5.25, 5.49, 8.85, 11.11, 11.11, 22.22 };
            List<SeatReservation> seatReservations = new List<SeatReservation>
        {
            new SeatReservation
            {
                Id = 1,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 2,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 3,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 4,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 5,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            },new SeatReservation
            {
                Id = 6,
                isReserved= true,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            }
        };
            PaymentData paymentData = new PaymentData(_DOTPAY_PIN, _DOTPAY_ID, listOfPrices, seatReservations);

            // act
            var result = paymentData.getPaymentData();

            // assert
            result.chk.Should().Be("076129e7555f90196270c5a7641bd54544f3fc9ade2df91c8231fa5ce0af144c");
            result.description.Should().Be("SeatReservationsIds:{1,2,3,4,5,6,}");
            result.amount.Should().Be("64,03");
            result.currency.Should().Be("USD");
        }

        public ResponseFromPaymentTransaction getPaymentData()
        {
            return new ResponseFromPaymentTransaction()
            {
                operation_number = "12345-567",
                operation_type = "payment",
                operation_status = "completed",
                operation_amount = "436.39",
                operation_currency = "PLN",
                operation_original_amount = "100.00",
                operation_original_currency = "USD",
                operation_datetime = DateTime.UtcNow.ToString(),
                control = "",
                description = "SeatReservationsIds::2::",
                email = "some@test.com",
                p_info = "User Ben Stiller",
                p_email = "b.stiller@test.test",
                channel = "1"
            };
        }        
        [Fact]
        public void PaymentServerResponse_isValid_ReturnTrue()
        {
            // arrange
            ResponseFromPaymentTransaction paymentResponse = getPaymentData();
            var mockSignature = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse).createResponseSignature();
            paymentResponse.signature = mockSignature;
            var testPaymentClass = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse);

            // act
            bool response = testPaymentClass.isValid();

            // assert
            response.Should().BeTrue();
        }
        [Fact]
        public void PaymentServerResponse_isValid_ReturnFalseBcuzWrongSignature()
        {
            // arrange
            ResponseFromPaymentTransaction paymentResponse = getPaymentData();
            var mockSignature = "wrong-signature";
            paymentResponse.signature = mockSignature;
            var testPaymentClass = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse);

            // act
            bool response = testPaymentClass.isValid();

            // assert
            response.Should().BeFalse();
        }
        [Fact]
        public void PaymentServerResponse_isValid_ReturnFalseBcuzPaymentUnsuccessfull()
        {
            // arrange
            ResponseFromPaymentTransaction paymentResponse = getPaymentData();
            paymentResponse.operation_status = "not-completed";
            var mockSignature = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse).createResponseSignature();
            paymentResponse.signature = mockSignature;
            var testPaymentClass = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse);

            // act
            bool response = testPaymentClass.isValid();

            // assert
            response.Should().BeFalse();
        }
        [Fact]
        public void PaymentServerResponse_isValid_ReturnFalseBcuzPaymentUnsuccessfullAndWrongSignature()
        {
            // arrange
            ResponseFromPaymentTransaction paymentResponse = getPaymentData();
            paymentResponse.operation_status = "not-completed";
            var mockSignature = "wrong-signature";
            paymentResponse.signature = mockSignature;
            var testPaymentClass = new PaymentServerResponse(_DOTPAY_PIN, paymentResponse);

            // act
            bool response = testPaymentClass.isValid();

            // assert
            response.Should().BeFalse();
        }
    }
}
