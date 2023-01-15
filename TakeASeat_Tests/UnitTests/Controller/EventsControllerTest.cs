using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Controllers;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventService;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class EventsControllerTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventsControllerTest()
        {
            _eventRepository = A.Fake<IEventRepository>();
            _mapper = A.Fake<IMapper>();
        }
        /// [Fact]
        /// public void Class_Method_Result       

        [Fact]
        public void EventsController_GetEvents_Return200()
        {
            // arrange
            RequestEventParams requestParams = new RequestEventParams();
            requestParams.PageNumber = 1;
            requestParams.PageSize = 10;
            requestParams.SearchString = "xxx";
            requestParams.EventTypes = new List<string>() { "Movie" };
            requestParams.OrderBy = "-name";
            EventsController controller = new EventsController(_mapper, _eventRepository);

            // act
            var response = controller.GetEvents(requestParams);

            // assert           
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }
        [Fact]
        public void EventsController_GetEventsByUser_Return200()
        {
            // arrange
            RequestEventParams requestParams = new RequestEventParams();
            requestParams.PageNumber = 0;
            requestParams.PageSize = 10;
            requestParams.SearchString = string.Empty;
            requestParams.EventTypes = new List<string>() { "Movie" };
            requestParams.OrderBy = "-name";
            string userName = "name";
            EventsController controller = new EventsController(_mapper, _eventRepository);

            // act
            var response = controller.GetEventsByUser(requestParams, userName);

            // assert           
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }

        [Fact]
        public void EventsController_GetEventsByUser_Return404_1()
        {
            // arrange
            RequestEventParams requestParams = new RequestEventParams();
            requestParams.PageNumber = 0;
            requestParams.PageSize = 10;
            requestParams.SearchString = string.Empty;
            requestParams.EventTypes = new List<string>() { "Movie" };
            requestParams.OrderBy = "-name";
            string userName = "";
            EventsController controller = new EventsController(_mapper, _eventRepository);

            // act
            var response = controller.GetEventsByUser(requestParams, userName);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(404, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

        [Fact]
        public void EventsController_GetEventsByUser_Return404_2()
        {
            // arrange
            RequestEventParams requestParams = new RequestEventParams();
            requestParams.PageNumber = 0;
            requestParams.PageSize = 10;
            requestParams.SearchString = string.Empty;
            requestParams.EventTypes = new List<string>() { "Movie" };
            requestParams.OrderBy = "-name";
            string userName = null;
            EventsController controller = new EventsController(_mapper, _eventRepository);

            // act
            var response = controller.GetEventsByUser(requestParams, userName);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(404, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void EventsController_GetEventRecordsNumber_Return200()
        {
            // arrange            
            EventsController controller = new EventsController(_mapper, _eventRepository);

            // act
            var response = controller.GetEventRecordsNumber();

            // assert           
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }

    }
}
