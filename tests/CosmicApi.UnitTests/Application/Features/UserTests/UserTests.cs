using AutoMapper;

using MockQueryable.Moq;
using Moq;
using Xunit;
using Xunit.Categories;

using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Application.MappingProfiles;
using CosmicApi.Application.Features.Auth.Authenticate;
using CosmicApi.Infrastructure.Services.TokenService;
using CosmicApi.Infrastructure.Common;
using CosmicApi.Application.Features.Users.GetUsers;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Galaxies;
using CosmicApi.Application.Features.Users.GetUserById;
using CosmicApi.Application.Features.Users;
using CosmicApi.Application.Features.Users.UpdatePassword;
using CosmicApi.Application.Common.Session;
using CosmicApi.UnitTests.Application.Helpers;

namespace CosmicApi.UnitTests.Application.Features.LoginTests
{

    [UnitTest]
    public class UserTests : FeatureTests
    {
        private IMapper _mapper;
        private List<User> _sourceUsers = new();
        private User _user = new();
        private Mock<IContext> context = new Mock<IContext>();
        private Mock<ITokenService> _tokenService = new();
        private Mock<ISession> _session = new Mock<ISession>();
        public UserTests()
        {
            _mapper = CreateMapper();
            _user.Email = "email@email.com";
            _user.Password = "password";
            _sourceUsers.Add(_user);

            _sourceUsers.AddRange(
                new List<User>()
                {
                    new User(){ Username = "A", Email = "A"},
                    new User(){ Username = "B", Email = "B"},
                    new User(){ Username = "C", Email = "C"},
                    new User(){ Username = "D", Email = "D"},
                    new User(){ Username = "E", Email = "E"},
                    new User(){ Username = "F", Email = "F"},
                    new User(){ Username = "G", Email = "G"},
                    new User(){ Username = "H", Email = "H"},
                    new User(){ Username = "J", Email = "J"},
                    new User(){ Username = "K", Email = "K"},
                    new User(){ Username = "L", Email = "L"},
                    new User(){ Username = "M", Email = "M"},
                    new User(){ Username = "N", Email = "N"},
                });
            var mockDbsetUsers = _sourceUsers.AsQueryable().BuildMockDbSet();

            context.Setup(x => x.Users).Returns(mockDbsetUsers.Object);

            _tokenService.Setup(x => x.GenerateAccessToken(It.IsAny<User>())).ReturnsAsync(
                new Jwt() { Token = "", ExpDate = DateTime.UtcNow, RefreshToken = "" }
                );

            _session.Setup(x => x.UserId).Returns(_user.Id);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(1, 10)]
        [InlineData(2, 5)]
        public async void ShouldReturn_UsersPaginatedResponse(int currentPage, int pageSize)
        {
            // Arrange
            GetUsersRequest request = new GetUsersRequest()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
            };
            GetUsersHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<PaginatedList<UserResponse>>(response);
            Assert.Equivalent(response.Result.Count, request.PageSize);
            Assert.Equivalent(response.CurrentPage, request.CurrentPage);
        }

        [Fact]
        public async void ShouldReturn_UserWhenExists()
        {
            // Arrange
            var userId = _sourceUsers.First().Id;
            GetUserByIdRequest request = new GetUserByIdRequest(userId);
            GetUserByIdHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<UserResponse>(response);
            Assert.Equivalent(response.Id, userId);
        }

        [Fact]
        public async void ShouldReturn_Null_WhenDoestExist()
        {
            // Arrange
            var userId = _user.Id;
            GetUserByIdRequest request = new GetUserByIdRequest(userId);
            GetUserByIdHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Should_UpdatePassword_WhenAuthenticated()
        {
            // Arrange
            var userId = _user.Id;
            UpdatePasswordRequest request = new UpdatePasswordRequest()
            {
                Password = "new-password",
            };
            UpdatePasswordHandler handler = new(_mapper, context.Object, _session.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.IsType<UserResponse>(response);
            Assert.Equivalent(response.Id, userId);
        }

    }
}

