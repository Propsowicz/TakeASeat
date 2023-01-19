using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Controllers;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using TakeASeat.Services.SeatReservationService;
using TakeASeat.Services.SeatService;
using TakeASeat.Services.ShowService;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class SeatsControllerTest
    {
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IShowRepository _showRepository;
        private readonly ISeatResRepository _seatReservationRepository;

        public SeatsControllerTest()
        {
            _mapper = A.Fake<IMapper>();
            _seatRepository = A.Fake<ISeatRepository>();
            _showRepository = A.Fake<IShowRepository>();
            _seatReservationRepository = A.Fake<ISeatResRepository>();
        }

        [Fact]
        public void SeatsController_GetSeats_Return400()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            int showId = 0;

            // act
            var response = controller.GetSeats(showId);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void SeatsController_GetSeats_Return200()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            int showId = 1;

            // act
            var response = controller.GetSeats(showId);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void SeatsController_CreateMultipleSeats_Return400()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            IEnumerable<CreateSeatDTO> seatsDTO = A.Fake<IEnumerable<CreateSeatDTO>>();

            // act
            var response = controller.CreateMultipleSeats(seatsDTO);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void SeatsController_CreateMultipleSeats_Return201()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            IEnumerable<CreateSeatDTO> seatsDTO = new List<CreateSeatDTO>()
            {
                new CreateSeatDTO
                {
                    ShowId = 1,
                    Row = 'A',
                    Position  = 1,
                    Price = 5.0,
                    SeatColor = "blue"
                }, new CreateSeatDTO
                {
                    ShowId = 2,
                    Row = 'A',
                    Position  = 2,
                    Price = 5.0,
                    SeatColor = "blue"
                }
            };

            // act
            var response = controller.CreateMultipleSeats(seatsDTO);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(201, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void SeatsController_RemoveSeatReservation_Return400()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            RequestSeatParams requestParams = new RequestSeatParams() { SeatId = 0 };

            // act
            var response = controller.RemoveSeatReservation(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void SeatsController_RemoveSeatReservation_Return204()
        {
            // arrange
            SeatsController controller = new SeatsController(_mapper, _seatRepository, _showRepository, _seatReservationRepository);
            RequestSeatParams requestParams = new RequestSeatParams() { SeatId = 1 };

            // act
            var response = controller.RemoveSeatReservation(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(204, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

    }
}

