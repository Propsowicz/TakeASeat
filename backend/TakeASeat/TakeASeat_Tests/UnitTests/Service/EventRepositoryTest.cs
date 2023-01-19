using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventService;
using TakeASeat.Services.EventTagRepository;
using TakeASeat_Tests.UnitTests.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class EventRepositoryTest
    {

        private readonly IEventTagRepository _eventTagRepository;
        private readonly IMapper _mapper;
        private readonly DatabaseContextMock _DbMock;

        public EventRepositoryTest()
        {
            _eventTagRepository = A.Fake<IEventTagRepository>();
            _mapper = A.Fake<IMapper>();
            _DbMock = new DatabaseContextMock();
        }      

        [Fact]
        public async void EventRepository_GetEventRecordsNumber_ReturnNumberOfRecords()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);

            // act
            var response = await repository.getEventRecordsNumber();

            // assert
            response.Should().Be(context.Events.ToList().Count());
        }
        [Fact]
        public async void EventRepository_GetEventsByUser_ReturnEmpty()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);
            RequestEventParams requestParams = new RequestEventParams()
            {
                PageSize = 1000,
                PageNumber = 1,
                OrderBy = "name",
                SearchString = string.Empty,
            };
            string userName = "TestUser";

            // act
            var response = await repository.getEventsByUser(requestParams, userName);

            // assert
            Assert.Equal(10, requestParams.PageSize);
            response.Should().BeEmpty();
        }
        [Fact]
        public async void EventRepository_GetEventsByUser_ReturnTwoListItems()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);
            RequestEventParams requestParams = new RequestEventParams()
            {
                PageSize = 1000,
                PageNumber = 1,
                OrderBy = "name",
                SearchString = string.Empty,
            };
            string userName = "LOG";

            // act
            var response = await repository.getEventsByUser(requestParams, userName);

            // assert
            Assert.Equal(10, requestParams.PageSize);
            response.Should().HaveCount(context.Events.Where(e => e.Creator.UserName == "LOG").Count());
        }
        [Fact]
        public async void EventRepository_CreateEvent_ReturnEventItem()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            CreateEventDTO eventDTO = new CreateEventDTO()
            {
                Name = "Test Event",
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
                CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            };
            Event eventObj = new Event()
            {
                Name = "Test Event",
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
                CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            };
            A.CallTo(() => _mapper.Map<Event>(eventDTO)).Returns(eventObj);
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);

            // act
            var response = await repository.createEvent(eventDTO);

            // assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Event>();
            response.Name.Should().Be(eventDTO.Name);
        }
        [Fact]
        public async void EventRepository_CreateEvent_ReturnNull()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            CreateEventDTO eventDTO = new CreateEventDTO()
            {
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
                CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            };
            Event eventObj = new Event()
            {
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
                CreatorId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            };
            A.CallTo(() => _mapper.Map<Event>(eventDTO)).Returns(eventObj);
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);

            // act
            var response = await repository.createEvent(eventDTO);

            // assert
            response.Should().BeNull();
        }
        [Fact]
        public async void EventRepository_EditEvent_ReturnOldNameNotEqualNewName()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            string oldName = context.Events.FirstOrDefault(e => e.Id == 3).Name;
            EditEventDTO eventDTO = new EditEventDTO()
            {
                Id = 3,
                Name = "New Test Event",
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
            };
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);

            // act
            var response = await repository.editEvent(eventDTO);

            // assert
            response.Should().NotBeNull();
            response.Name.Should().NotBe(oldName);
        }

        [Fact]
        public async void EventRepository_EditEvent_ReturnNull()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EditEventDTO eventDTO = new EditEventDTO()
            {
                Id = 6,
                Description = "Event Test Description",
                Duration = 120,
                Place = "Warsaw",
                ImageUrl = string.Empty,
                EventTypeId = 1,
            };
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);

            // act
            var response = await repository.editEvent(eventDTO);

            // assert
            response.Should().BeNull();
        }
        [Fact]
        public async void EventRepository_DeleteEvent_ReturnOK()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventRepository repository = new EventRepository(context, _mapper, _eventTagRepository);
            int eventId = 5;
            int oldEventsNumber = context.Events.ToList().Count();

            // act
            var response = repository.deleteEvent(eventId);

            // assert
            int newEventsNumber = context.Events.ToList().Count();
            Assert.NotEqual(oldEventsNumber, newEventsNumber);
        }


        // raw code tests
        [Fact]
        public void EventRepository_CreateEvent_Slugify()
        {
            // arrange
            string eventName = "Some String";

            // act
            string eventSlug = eventName.ToLower().Replace(" ", "-");

            // assert
            Assert.Equal("some-string", eventSlug);
        }
        [Fact]
        public void EventRepository_DeleteEvent_IsAnyShowReadyToSellTrue()
        {
            // arrange
            List<string> queryValidation = new List<string>() { "one", "two" };
            string query;
            string result;
            string resultNull = "notNull";

            // act
            if (!EventValidation.IsAnyShowReadyToSell(queryValidation.Count()))
            {
                query = "queryExisting";
                if (query == null)
                {
                    resultNull = "nullException";
                }

                result = "OK";
            }
            else
            {
                result = "Can't delete event with shows which are ready to sell.";
            }

            // assert
            Assert.Equal("Can't delete event with shows which are ready to sell.", result);
        }
        [Fact]
        public void EventRepository_DeleteEvent_IsAnyShowReadyToSellFalse()
        {
            // arrange
            List<string> queryValidation = new List<string>() { };
            string query;
            string result;
            string resultNull = "notNull";

            // act
            if (!EventValidation.IsAnyShowReadyToSell(queryValidation.Count()))
            {
                query = "queryExisting";
                if (query == null)
                {
                    resultNull = "nullException";
                }

                result = "OK";
            }
            else
            {
                result = "Can't delete event with shows which are ready to sell.";
            }

            // assert
            Assert.Equal("OK", result);
            Assert.Equal("notNull", resultNull);
        }
        [Fact]
        public void EventRepository_DeleteEvent_IsAnyShowReadyToSellFalseQueryNull()
        {
            // arrange
            List<string> queryValidation = new List<string>() { };
            string query;
            string result;
            string resultNull = "notNull";

            // act
            if (!EventValidation.IsAnyShowReadyToSell(queryValidation.Count()))
            {
                query = null;
                if (query == null)
                {
                    resultNull = "nullException";
                }

                result = "OK";
            }
            else
            {
                result = "Can't delete event with shows which are ready to sell.";
            }

            // assert
            Assert.Equal("nullException", resultNull);
        }
    }
}
