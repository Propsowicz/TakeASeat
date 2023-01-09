using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeASeat.Models;
using TakeASeat_Tests.Utils;

namespace TakeASeat_Tests.Models
{
    public class EventDTOTest
    {       
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnNameIsTooShort()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "012345678",                
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );

            // assert
            Assert.True(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnNameIsTooLong()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0",
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );

            // assert
            Assert.True(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnNameOKWith100Characters()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789" +
                        "0123456789"                         
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );

            // assert
            Assert.False(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnNameIsOKWith10Characters()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Name = "0123456789"                         
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );

            // assert
            Assert.False(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnDurationUnderLimitRange()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Duration = 10
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO);

            // assert
            Assert.NotEmpty(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnDurationAboveLimitRange()
        {
            // arrange
            var eventDTO = new CreateEventDTO()
            {
                Duration = 100000
            };

            // act
            var result = DTOValidation.ValidateObject(eventDTO);

            // assert
            Assert.NotEmpty(result);
        }
        [Fact]
        public void EventDTO_CreateEventDTO_ReturnDurationOK()
        {
            // arrange
            var eventDTO_1 = new CreateEventDTO()
            {
                Duration = 30
            };
            var eventDTO_2 = new CreateEventDTO()
            {
                Duration = 240
            };

            // act
            var result_1 = DTOValidation.ValidateObject(eventDTO_1).Where(v => v.MemberNames.Equals("Duration"));
            var result_2 = DTOValidation.ValidateObject(eventDTO_2).Where(v => v.MemberNames.Equals("Duration"));

            // assert
            Assert.Empty(result_1);
            Assert.Empty(result_2);
        }

    }
}
