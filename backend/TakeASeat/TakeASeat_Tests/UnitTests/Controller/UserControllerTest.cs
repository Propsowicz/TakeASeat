using FakeItEasy;
using FluentAssertions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TakeASeat.Data;
using TakeASeat.Services.UserService;
using TakeASeat.Controllers;
using TakeASeat.Models;
using Microsoft.AspNetCore.Mvc;

namespace TakeASeat_Tests.UnitTests.Controller
{
    public class UserControllerTest
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;

        public UserControllerTest()
        {
            _userManager = A.Fake<UserManager<User>>();
            _mapper = A.Fake<IMapper>();
            _authManager = A.Fake<IAuthManager>();
            _userRepository = A.Fake<IUserRepository>();
        }

        [Fact]
        public async Task UserController_RegisterUser_Return201()
        {
            // arrange
            var controller = new UserController(_userManager, _mapper, _authManager, _userRepository);
            var newUser = new RegisterUserDTO()
            {
                Email = "some@email.com",
                UserName = "testOne",
                FirstName = "Tester",
                LastName = "Testerowitzh",
                Password = "Password123!"
            };

            // act
            var response = controller.RegisterUser(newUser);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(201, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
        [Fact]
        public async Task UserController_RegisterUser_Return400()
        {
            // arrange
            var controller = new UserController(_userManager, _mapper, _authManager, _userRepository);
            var newUser = new RegisterUserDTO()
            {
                Email = "some@email.com",
                UserName = "testOne",
                FirstName = "Tester",
                LastName = "Testerowitzh",
            };

            // act
            var response = controller.RegisterUser(newUser);

            // assert
            var result = response.Result;
            StatusCodeResult objectResult = Assert.IsType<StatusCodeResult>(result);
            response.Should().NotBeNull();
            Assert.Equal(400, objectResult.StatusCode);
            objectResult.Should().BeOfType(typeof(StatusCodeResult));
        }
    }
}
