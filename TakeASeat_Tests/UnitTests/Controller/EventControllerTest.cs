using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services.EventService;
using FakeItEasy;
using TakeASeat.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using System.ComponentModel.DataAnnotations;
using TakeASeat.RequestUtils;
using TakeASeat_Tests.UnitTests.Utils;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class EventControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        public EventControllerTest()
        {
            _eventRepository = A.Fake<IEventRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void EventController_GetEvent_Return200()
        {
            // arrange
            int eventId = 1;
            EventController controller = new EventController(_mapper, _eventRepository);

            // act
            var response = controller.GetEvent(eventId);

            // assert
            var result = response.Result;
            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(ObjectResult));
        }

        [Fact]
        public void EventController_GetEvent_Return400_1()
        {
            // arrange
            int eventId = 0;
            EventController controller = new EventController(_mapper, _eventRepository);

            // act
            var response = controller.GetEvent(eventId);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

        [Fact]
        public void EventController_GetEvent_Return400_2()
        {
            // arrange
            int eventId = -5;
            EventController controller = new EventController(_mapper, _eventRepository);

            // act
            var response = controller.GetEvent(eventId);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

        [Fact]
        public void EventController_CreateEvent_Return201()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "0123456789",
                Description = "0123456789",
                Duration = 100,
                ImageUrl = "0123456789",
                EventTypeId = 1,
                CreatorId = "0123456789",
                Place = "0123456789"
            };
            var eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO { Id = 1, TagName= "test" }
            };
            var requestParams = new CreateEventWithTagsDTO()
            {
                eventDTO = eventDTO,
                eventTagsDTO = eventTagsDTO
            };
            EventController controller = new EventController(_mapper, _eventRepository);

            // act            
            var listOfEventDtoValidationErrors = DTOValidation.CheckForErrors(eventDTO);
            var response = controller.CreateEvent(requestParams);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(201, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));

            Assert.Empty(listOfEventDtoValidationErrors);
        }
        [Fact]
        public void EventController_CreateEvent_ReturnInvalidDTO_1()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "0123456789",
                Description = "0123456789",
                Duration = 10,                      // int range 30 - 240
                ImageUrl = "0123456789",
                EventTypeId = 1,
                CreatorId = "0123456789",
                Place = "0123456789"
            };
            var eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO { Id = 1, TagName= "test" }
            };
            var requestParams = new CreateEventWithTagsDTO()
            {
                eventDTO = eventDTO,
                eventTagsDTO = eventTagsDTO
            };

            // act            
            var listOfEventDtoValidationErrors = DTOValidation.CheckForErrors(eventDTO);

            // assert         
            Assert.NotEmpty(listOfEventDtoValidationErrors);
            Assert.Equal(1, listOfEventDtoValidationErrors.Count());
        }
        [Fact]
        public void EventController_CreateEvent_ReturnInvalidDTO_2()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "123",                       // min length 10
                Description = "0123456789",
                Duration = 10,                      // int range 30 - 240
                ImageUrl = "0123456789",
                EventTypeId = 1,
                CreatorId = "0123456789",
                Place = "1"                         // min length 2
            };
            var eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO { Id = 1, TagName= "test" }
            };
            var requestParams = new CreateEventWithTagsDTO()
            {
                eventDTO = eventDTO,
                eventTagsDTO = eventTagsDTO
            };

            // act            
            var listOfEventDtoValidationErrors = DTOValidation.CheckForErrors(eventDTO);

            // assert         
            Assert.NotEmpty(listOfEventDtoValidationErrors);
            Assert.Equal(3, listOfEventDtoValidationErrors.Count());
        }

        [Fact]
        public void EventController_EditEvent_Return200()
        {
            // arrange
            var eventDTO = new EditEventDTO()
            {
                Id = 1,
                Name = "0123456789",
                Description = "0123456789",
                Duration = 150,
                ImageUrl = "0123456789",
                EventTypeId = 1,
                Place = "0123456789"
            };
            var eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO { Id = 1, TagName= "test" }
            };
            var requestParams = new EditEventWithTagsDTO()
            {
                eventDTO = eventDTO,
                eventTagsDTO = eventTagsDTO
            };
            EventController controller = new EventController(_mapper, _eventRepository);

            // act            
            var response = controller.EditEvent(requestParams);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void EventController_EditEvent_Return400()
        {
            // arrange
            var eventDTO = new EditEventDTO()
            {
                Id = 0,
                Name = "0123456789",
                Description = "0123456789",
                Duration = 150,
                ImageUrl = "0123456789",
                EventTypeId = 1,
                Place = "0123456789"
            };
            var eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO { Id = 1, TagName= "test" }
            };
            var requestParams = new EditEventWithTagsDTO()
            {
                eventDTO = eventDTO,
                eventTagsDTO = eventTagsDTO
            };
            EventController controller = new EventController(_mapper, _eventRepository);

            // act            
            var response = controller.EditEvent(requestParams);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

        [Fact]
        public void EventController_DeleteEvent_Return200()
        {
            // arrange
            RequestEventDeleteParams requestParams = new RequestEventDeleteParams()
            {
                EventId = 1
            }; EventController controller = new EventController(_mapper, _eventRepository);

            // act            
            var response = controller.DeleteEvent(requestParams);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(200, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public void EventController_DeleteEvent_Return400()
        {
            // arrange
            RequestEventDeleteParams requestParams = new RequestEventDeleteParams()
            {
                EventId = 0
            };
            EventController controller = new EventController(_mapper, _eventRepository);

            // act            
            var response = controller.DeleteEvent(requestParams);

            // assert           
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);

            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }

    }
}
