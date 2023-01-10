using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Services.EventTagRepository;
using TakeASeat_Tests.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.EventService;
using TakeASeat.Services.ShowService;
using TakeASeat.Data;

namespace TakeASeat_Tests.Service
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
            int showId = 1;

            // act
            var response = await repository.GetShowDetails(showId);

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
            var response = await repository.GetShowDetails(showId);

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
            var response = repository.SetShowReadyToSell(showId);

            // assert
            bool isReadyToSellNew = context.Shows.FirstOrDefault(x => x.Id == showId).IsReadyToSell;
            isReadyToSellOld.Should().BeFalse();    
            isReadyToSellNew.Should().BeTrue();    
        }
        
    }
}
