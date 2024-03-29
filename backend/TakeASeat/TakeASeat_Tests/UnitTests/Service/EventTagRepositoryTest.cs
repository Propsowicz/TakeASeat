﻿using FakeItEasy;
using FluentAssertions;
using TakeASeat.Models;
using TakeASeat.Services.EventTagRepository;
using TakeASeat_Tests.UnitTests.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class EventTagRepositoryTest
    {
        
        private readonly DatabaseContextMock _DbMock;

        public EventTagRepositoryTest()
        {
            _DbMock = new DatabaseContextMock();
        }        

        [Fact]
        public async Task EventTagRepository_AddEventTags_ShouldAddTagToEvent()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventTagRepository repository = new EventTagRepository(context);
            List<GetEventTagDTO> eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO()
                {
                    Id = 1,
                    TagName = "#AnimatedMovie",
                },
                new GetEventTagDTO()
                {
                    Id = 2,
                    TagName = "#FamilyFriendly",
                }
            };
            int eventId = 3;
            int oldTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();

            // act 
            var response = repository.addEventTags(eventTagsDTO, eventId);

            // assert
            int newTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();
            newTagsNumber.Should().BeGreaterThan(oldTagsNumber);
        }
        [Fact]
        public async Task EventTagRepository_AddEventTags_ShouldNotAddTagToEvent()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventTagRepository repository = new EventTagRepository(context);
            List<GetEventTagDTO> eventTagsDTO = new List<GetEventTagDTO>()
            {
                new GetEventTagDTO()
                {
                    Id = 1,
                    TagName = "#AnimatedMovie",
                },
                new GetEventTagDTO()
                {
                    Id = 2,
                    TagName = "#FamilyFriendly",
                }
            };
            int eventId = 0;
            int oldTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();

            // act 
            var response = repository.addEventTags(eventTagsDTO, eventId);

            // assert
            int newTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();
            newTagsNumber.Should().Be(oldTagsNumber);
        }
        [Fact]
        public async Task EventTagRepository_RemoveEventTags_ShouldRemoveAllTags()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            EventTagRepository repository = new EventTagRepository(context);
            int eventId = 1;
            int oldTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();

            // act 
            var response = repository.removeEventTags(eventId);
            context.SaveChanges();                                          // need to save here (didnt create unitOfWork)

            // assert
            int newTagsNumber = context.EventTagEventM2M.Where(t => t.EventId == eventId).Count();
            newTagsNumber.Should().Be(0);
        }

    }
}
