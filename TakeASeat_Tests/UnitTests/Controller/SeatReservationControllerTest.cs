using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services.SeatReservationService;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Data;
using TakeASeat.Controllers;
using TakeASeat.RequestUtils;
using Microsoft.AspNetCore.Mvc;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class SeatReservationControllerTest
    {
        private readonly IMapper _mapper;
        private readonly ISeatResRepository _seatResRepository;

        public SeatReservationControllerTest()
        {
            _mapper = A.Fake<IMapper>();
            _seatResRepository = A.Fake<ISeatResRepository>();
        }
        [Fact]
        public void SeatReservationController_DeleteSeatReservation_Return400()
        {
            // arrange
            SeatsReservationController controller = new SeatsReservationController(_mapper, _seatResRepository);
            RequestReservationParams requestParams = new RequestReservationParams() { seatReservationId = 0 };

            // act
            var response = controller.DeleteSeatReservation(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void SeatReservationController_DeleteSeatReservation_Return204()
        {
            // arrange
            SeatsReservationController controller = new SeatsReservationController(_mapper, _seatResRepository);
            RequestReservationParams requestParams = new RequestReservationParams() { seatReservationId = 1 };

            // act
            var response = controller.DeleteSeatReservation(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(204, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
    }
}
