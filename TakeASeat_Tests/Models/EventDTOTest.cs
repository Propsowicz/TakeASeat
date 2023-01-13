using FluentAssertions;
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
        // these tests check dto models validators and integrity between create and edit model validators

        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnNameIsTooShort()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
            {
                Name = "012345678",                
            };
            var editEventDTO = new EditEventDTO()
            {
                Name = "012345678",
            };

            // act
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );

            // assert
            resultCreate.Should().BeTrue();
            resultEdit.Should().BeTrue();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnNameIsTooLong()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
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
            var editEventDTO = new EditEventDTO()
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
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );

            // assert
            resultCreate.Should().BeTrue();
            resultEdit.Should().BeTrue();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnNameOKWith100Characters()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
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
            var editEventDTO = new EditEventDTO()
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
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too long.")
                );

            // assert
            resultCreate.Should().BeFalse();
            resultEdit.Should().BeFalse();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnNameIsOKWith10Characters()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
            {
                Name = "0123456789"                         
            };
            var editEventDTO = new EditEventDTO()
            {
                Name = "0123456789"
            };

            // act
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO).Any(
                v => v.MemberNames.Contains("Name")
                && v.ErrorMessage.Contains("Name is too short.")
                );

            // assert
            resultCreate.Should().BeFalse();
            resultEdit.Should().BeFalse();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnDurationUnderLimitRange()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
            {
                Duration = 10
            };
            var editEventDTO = new EditEventDTO()
            { 
                Duration = 10
            };

            // act
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO);
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO);

            // assert
            resultCreate.Should().NotBeEmpty();
            resultEdit.Should().NotBeEmpty();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnDurationAboveLimitRange()
        {
            // arrange
            var createEventDTO = new CreateEventDTO()
            {
                Duration = 100000
            };
            var editEventDTO = new EditEventDTO()
            {
                Duration = 100000
            };

            // act
            var resultCreate = DTOValidation.CheckForErrors(createEventDTO);
            var resultEdit = DTOValidation.CheckForErrors(editEventDTO);

            // assert
            resultCreate.Should().NotBeEmpty();
            resultEdit.Should().NotBeEmpty();
        }
        [Fact]
        public void EventDTO_CreateEventDTOAndEditEventDTO_ReturnDurationOK()
        {
            // arrange
            var eventDTO_1_create = new CreateEventDTO()
            {
                Duration = 30
            };
            var eventDTO_2_create = new CreateEventDTO()
            {
                Duration = 240
            };
            var eventDTO_1_edit = new EditEventDTO()
            {
                Duration = 30
            };
            var eventDTO_2_edit = new EditEventDTO()
            {
                Duration = 240
            };

            // act
            var result_1_create = DTOValidation.CheckForErrors(eventDTO_1_create).Where(v => v.MemberNames.Equals("Duration"));
            var result_2_create = DTOValidation.CheckForErrors(eventDTO_2_create).Where(v => v.MemberNames.Equals("Duration"));
            var result_1_edit = DTOValidation.CheckForErrors(eventDTO_1_edit).Where(v => v.MemberNames.Equals("Duration"));
            var result_2_edit = DTOValidation.CheckForErrors(eventDTO_2_edit).Where(v => v.MemberNames.Equals("Duration"));

            // assert
            result_2_create.Should().BeEmpty();
            result_1_create.Should().BeEmpty();
            result_2_edit.Should().BeEmpty();
            result_1_edit.Should().BeEmpty();

        }

    }
}
