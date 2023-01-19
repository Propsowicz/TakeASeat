using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Controllers;
using TakeASeat.Services.ShowService;
using FluentAssertions;
using TakeASeat.RequestUtils;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class ShowsControllerTest
    {
        public readonly IMapper _mapper;
        public readonly IShowRepository _showRepository;

        public ShowsControllerTest()
        {
            _mapper = A.Fake<IMapper>();
            _showRepository = A.Fake<IShowRepository>();
        }

        [Fact]
        public void ShowsController_GetShowsToHomePage_Return200()
        {
            // arrange
            ShowsController controller = new ShowsController(_mapper, _showRepository);

            // act
            var response = controller.GetShowsToHomePage();

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void ShowsController_GetShows_Return200()
        {
            // arrange
            ShowsController controller = new ShowsController(_mapper, _showRepository);
            RequestShowParams requestParams = A.Fake<RequestShowParams>();

            // act
            var response = controller.GetShows(requestParams);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void ShowsController_GetShowsByEventTags_Return200()
        {
            // arrange
            ShowsController controller = new ShowsController(_mapper, _showRepository);
            RequestTagsParams requestParams = A.Fake<RequestTagsParams>();

            // act
            var response = controller.GetShowsByEventTags(requestParams);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void ShowsController_GetShowsRecordNumber_Return200()
        {
            // arrange
            ShowsController controller = new ShowsController(_mapper, _showRepository);

            // act
            var response = controller.GetShowsRecordNumber();

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void ShowController_GetShowsByEvent_Return200()
        {
            // arrange
            int eventId = 1;
            ShowsController controller = new ShowsController(_mapper, _showRepository);

            // act
            var response = controller.GetShowsByEvent(eventId);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }

        [Fact]
        public void ShowController_GetShowsByEvent_Return400()
        {
            // arrange
            int eventId = 0;
            ShowsController controller = new ShowsController(_mapper, _showRepository);

            // act
            var response = controller.GetShowsByEvent(eventId);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
    }
}
