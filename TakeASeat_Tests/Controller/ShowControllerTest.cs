using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services.ShowService;
using FakeItEasy;
using TakeASeat.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Azure;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using TakeASeat.Models;
using TakeASeat.RequestUtils;

namespace TakeASeat_Tests.Controller
{
    public class ShowControllerTest
    {
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowControllerTest()
        {
            _mapper = A.Fake<IMapper>();
            _showRepository= A.Fake<IShowRepository>();
        }

        [Fact]
        public void ShowController_GetShow_Return200()
        {
            // arrange
            int showId = 1;
            ShowController controller = new ShowController(_mapper, _showRepository);

            // act
            var response = controller.GetShow(showId);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }

        [Fact]
        public void ShowController_GetShow_Return400()
        {
            // arrange
            int showId = 0;
            ShowController controller = new ShowController(_mapper, _showRepository);

            // act
            var response = controller.GetShow(showId);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void ShowController_CreateShow_Return201()
        {
            // arrange
            CreateShowDTO showDTO = A.Fake<CreateShowDTO>();
            ShowController controller = new ShowController(_mapper, _showRepository);

            // act
            var response = controller.CreateShow(showDTO);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(201, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void ShowController_DeleteShow_Return200()
        {
            // arrange
            DeleteShowParams requestParams = new DeleteShowParams() { ShowId= 1 };
            ShowController controller = new ShowController(_mapper, _showRepository);

            // act
            var response = controller.DeleteShow(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

        [Fact]
        public void ShowController_DeleteShow_Return400()
        {
            // arrange
            DeleteShowParams requestParams = new DeleteShowParams() { ShowId = 0 };
            ShowController controller = new ShowController(_mapper, _showRepository);

            // act
            var response = controller.DeleteShow(requestParams);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
    }
}
