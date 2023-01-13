using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TakeASeat.Models;
using TakeASeat_Tests.Utils;

namespace TakeASeat_Tests.Models
{
    public class ShowDTOTest
    {
        [Fact]
        public void ShowDTO_CreateShowDTO_DateInvalid_CaseYesterday ()
        {
            // arrange
            DateTime testDate = DateTime.UtcNow.AddDays(-1);
            var createShowDTO = new CreateShowDTO()
            {   
                EventId = 1,
                Date = testDate,
                Description = "Some Test Description"
            };

            // act
            var result = DTOValidation.CheckForErrors(createShowDTO).Any(
                v => v.MemberNames.Contains("Date")
                && v.ErrorMessage.Contains("Date should be at least five days from today."
                ));

            // assert
            result.Should().BeTrue();
        }
        [Fact]
        public void ShowDTO_CreateShowDTO_DateInvalid_CaseFourDaysFromToday()
        {
            // arrange
            DateTime testDate = DateTime.UtcNow.AddDays(4);
            var createShowDTO = new CreateShowDTO()
            {
                EventId = 1,
                Date = testDate,
                Description = "Some Test Description"
            };

            // act
            var result = DTOValidation.CheckForErrors(createShowDTO).Any(
                v => v.MemberNames.Contains("Date")
                && v.ErrorMessage.Contains("Date should be at least five days from today."
                ));

            // assert
            result.Should().BeTrue();
        }
        [Fact]
        public void ShowDTO_CreateShowDTO_DateValid_CaseFiveDaysFromToday()
        {
            // arrange
            DateTime testDate = DateTime.UtcNow.AddDays(5).AddMinutes(1);
            var createShowDTO = new CreateShowDTO()
            {
                EventId = 1,
                Date = testDate,
                Description = "Some Test Description"
            };

            // act
            var result = DTOValidation.CheckForErrors(createShowDTO).Any(
                v => v.MemberNames.Contains("Date")
                && v.ErrorMessage.Contains("Date should be at least five days from today."
                ));

            // assert
            result.Should().BeFalse();
        }

    }
}
