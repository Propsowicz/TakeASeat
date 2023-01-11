using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.PaymentService;

namespace TakeASeat_Tests.Service
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
            result.chk.Should().Be("90df21e3abe8dea81e86cdadd13d428932047da298b0cb899f7ebd2136e7a1fb");
            result.description.Should().Be("SeatReservationsIds::1::2::3::4::5::6::");
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
            result.chk.Should().Be("1730e5ef21f3b6e6027f78d8a785902e37ec4c1bff3e92140cd13fee34d5d190");
            result.description.Should().Be("SeatReservationsIds::1::2::3::4::5::6::");
            result.amount.Should().Be("64,03");
            result.currency.Should().Be("USD");
        }

    }
}
