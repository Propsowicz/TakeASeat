using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services.EventTagRepository;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.EventService;
using TakeASeat.Services.ShowService;
using TakeASeat.Data;
using X.PagedList;
using TakeASeat.Models;
using TakeASeat_Tests.UnitTests.Data;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data.DatabaseContext;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class ShowRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContextMock _DbMock;
        public ShowRepositoryTest()
        {
            _mapper = A.Fake<IMapper>();
            _DbMock = new DatabaseContextMock();
        }      

        [Fact]
        public async void ShowRepository_GetShowDetails_ReturnNotNull()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int showId = 2;

            // act
            var response = await repository.getShowDetails(showId);

            // assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Show>();
        }
        [Fact]
        public async void ShowRepository_GetShowDetails_ReturnNull()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int showId = 0;

            // act
            var response = await repository.getShowDetails(showId);

            // assert
            response.Should().BeNull();
        }
        [Fact]
        public async void ShowRepository_SetShowReadyToSell_IsReadyToSellShouldBeTrue()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int showId = 1;
            bool isReadyToSellOld = context.Shows.FirstOrDefault(x => x.Id == showId).IsReadyToSell;

            // act
            var response = repository.setShowReadyToSell(showId);

            // assert
            bool isReadyToSellNew = context.Shows.FirstOrDefault(x => x.Id == showId).IsReadyToSell;
            isReadyToSellOld.Should().BeFalse();
            isReadyToSellNew.Should().BeTrue();
        }
        [Fact]
        public async void ShowRepository_GetShowRecordNumber_ReturnNumber()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);

            // act
            var response = await repository.getShowRecordsNumber();

            // assert
            response.Should().BeOfType(typeof(int));
        }
        [Fact]
        public async void ShowRepository_GetShowsByEvent_ReturnQuery()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int eventId = 1;

            // act
            var response = await repository.getShowsByEvent(eventId);

            // assert
            response.Should().NotBeNull();
        }
        [Fact]
        public async void ShowRepository_GetShowsByEvent_ReturnNull()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int eventId = 0;

            // act
            var response = await repository.getShowsByEvent(eventId);

            // assert
            response.Should().BeNull();
        }
        [Fact]
        public async void ShowRepository_CreateShow_ShouldAddNewShow()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            CreateShowDTO showDTO = new CreateShowDTO()
            {
                EventId = 1,
                Date = new DateTime(2023, 02, 22, 15, 00, 00),
                Description = "Test Show",
            };
            Show showObj = new Show()
            {
                EventId = 1,
                Date = new DateTime(2023, 02, 22, 15, 00, 00),
                Description = "Test Show",
            };
            A.CallTo(() => _mapper.Map<Show>(showDTO)).Returns(showObj);
            int oldNumberOfShows = context.Shows.ToList().Count();

            // act
            var response = repository.createShow(showDTO);

            // assert
            int newNumberOfShows = context.Shows.ToList().Count();
            newNumberOfShows.Should().BeGreaterThan(oldNumberOfShows);
        }
        [Fact]
        public async void ShowRepository_CreateShow_ShouldNotAddNewShow()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            CreateShowDTO showDTO = new CreateShowDTO()
            {
                Date = new DateTime(2023, 02, 22, 15, 00, 00),
                Description = "Test Show",
            };
            Show showObj = new Show()
            {
                Date = new DateTime(2023, 02, 22, 15, 00, 00),
                Description = "Test Show",
            };
            A.CallTo(() => _mapper.Map<Show>(showDTO)).Returns(showObj);
            int oldNumberOfShows = context.Shows.ToList().Count();

            // act
            var response = repository.createShow(showDTO);

            // assert
            int newNumberOfShows = context.Shows.ToList().Count();
            newNumberOfShows.Should().Be(oldNumberOfShows);
        }
        [Fact]
        public async void ShowRepository_DeleteShow_ShouldDeleteOneShow()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int oldNumberOfShows = context.Shows.ToList().Count();
            int showId = 3;

            // act
            var response = repository.deleteShow(showId);

            // assert
            int newNumberOfShows = context.Shows.ToList().Count();
            newNumberOfShows.Should().BeLessThan(oldNumberOfShows);
        }
        [Fact]
        public async void ShowRepository_DeleteShow_ShouldNotDeleteShowBecouseIsReadyToSell()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int oldNumberOfShows = context.Shows.ToList().Count();
            int showId = 3;
            var showToEdit = context.Shows.FirstOrDefault(s => s.Id == showId);
            showToEdit.IsReadyToSell = true;

            // act
            var response = repository.deleteShow(showId);

            // assert
            int newNumberOfShows = context.Shows.ToList().Count();
            newNumberOfShows.Should().Be(oldNumberOfShows);
        }
        [Fact]
        public async void ShowRepository_DeleteShow_ShouldNotDeleteShowBecouseWrongId()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            ShowRepository repository = new ShowRepository(context, _mapper);
            int oldNumberOfShows = context.Shows.Count();
            int showId = 33333333;

            // act
            var response = repository.deleteShow(showId);

            // assert
            int newNumberOfShows = context.Shows.Count();
            newNumberOfShows.Should().Be(oldNumberOfShows);
        }
    }
}
