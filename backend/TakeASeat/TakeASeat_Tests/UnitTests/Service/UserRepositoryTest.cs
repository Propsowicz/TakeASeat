using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using FakeItEasy;
using FluentAssertions;
using TakeASeat.Services.UserService;
using TakeASeat.Models;
using TakeASeat_Tests.UnitTests.Data;

namespace TakeASeat_Tests.UnitTests.Service
{
    public class UserRepositoryTest
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContextMock _DbMock;
        public UserRepositoryTest()
        {
            _userManager = A.Fake<UserManager<User>>();
            _roleManager = A.Fake<RoleManager<IdentityRole>>();
            _DbMock = new DatabaseContextMock();
        }       

        [Fact]
        public async Task UserRepository_AddToRoleNamedUser_ShouldAddRoleNamedUser()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            var repository = new UserRepository(_userManager, context, _roleManager);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == "8e445865-a24d-4543-a6c6-9443d048cdb9");
            int oldNumberOfUserRoles = context.UserRoles.Where(ur => ur.UserId == user.Id).Count();

            // act 
            await repository.AddToRoleNamedUser(user);

            // assert
            int newNumberOfUserRoles = context.UserRoles.Where(ur => ur.UserId == user.Id).Count();
            oldNumberOfUserRoles.Should().Be(0);
            newNumberOfUserRoles.Should().Be(1);
        }
        [Fact]
        public async Task UserRepository_ChangeRoles_ShouldAddUserToAdminAndOrganizerRoles()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            var repository = new UserRepository(_userManager, context, _roleManager);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == "8e445865-a24d-4543-a6c6-9443d048cdb0");
            string adminRoleId = context.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;
            string organizerRoleId = context.Roles.FirstOrDefault(r => r.Name == "Organizer").Id;
            EditUserRolesDTO userDTO = new EditUserRolesDTO()
            {
                UserId = user.Id,
                UserRoles = new List<GetRoleDTO>()
                {
                    new GetRoleDTO()
                    {
                        Id = adminRoleId,
                        Name = "Administrator"
                    },
                    new GetRoleDTO()
                    {
                        Id = organizerRoleId,
                        Name = "Organizer"
                    }
                }
            };

            // act 
            await repository.ChangeRoles(userDTO);

            // assert
            var userRoles = await context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
            userRoles.Should().HaveCount(2);
            userRoles[0].Role.Name.Should().BeOneOf("Organizer", "Administrator");
        }
        [Fact]
        public async Task UserRepository_GetUsersRecordsNumber_ReturnNumberTwo()
        {
            // arrange
            var context = await _DbMock.GetDatabaseContext();
            var repository = new UserRepository(_userManager, context, _roleManager);


            // act 
            var response = await repository.GetUsersRecordsNumber();

            // assert
            response.Should().Be(2);
        }

    }
}
