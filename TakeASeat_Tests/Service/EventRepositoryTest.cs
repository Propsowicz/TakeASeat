using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventService;
using TakeASeat.Services.EventTagRepository;

namespace TakeASeat_Tests.Service
{
    public class EventRepositoryTest
    {
                
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
