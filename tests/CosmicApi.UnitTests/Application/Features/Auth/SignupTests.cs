using AutoMapper;
using BC = BCrypt.Net.BCrypt;

using MockQueryable.Moq;
using Moq;
using Xunit;
using Xunit.Categories;

using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Infrastructure.Services.TokenService;
using CosmicApi.Infrastructure.Common;
using CosmicApi.Application.Features.Auth.Signup;
using CosmicApi.UnitTests.Application.Helpers;

namespace CosmicApi.UnitTests.Application.Features.LoginTests
{

    [UnitTest]
    public class SignupTests : FeatureTests
    {
        private IMapper _mapper;
        private List<User> _sourceUsers = new();
        private User _user = new();
        private Mock<IContext> context = new Mock<IContext>();
        private Mock<ITokenService> _tokenService = new();
        public SignupTests()
        {
            _mapper = CreateMapper();
            
            // setup user
            _user.Email = "email@email.com";
            _user.Password = "password";
            _user.Username = "username";

            _sourceUsers.Add(_user);
            // users mock dbset
            var mockDbsetUsers = _sourceUsers.AsQueryable().BuildMockDbSet();

            context.Setup(x => x.Users).Returns(mockDbsetUsers.Object);

            _tokenService.Setup(x => x.GenerateAccessToken(It.IsAny<User>())).ReturnsAsync(
                new Jwt() { Token = "", ExpDate = DateTime.UtcNow, RefreshToken = "" }
                );

        }

        [Fact]
        public async void ShouldReturn_Success_WhenUiqueEmailAndUsername()
        {         
            // Arrange
            SignupRequest request = new SignupRequest()
            {
                Username = "unique.username",
                Email = "unique.email@email.com",
                Password = "unique.password",
            };

            SignupHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.False(response.Errors.Any());
            Assert.NotNull(response.Value);
        }

        [Fact]
        public async void ShouldReturn_Failure_WhenUsernameAlreadyExists()
        {
            // Arrange
            SignupRequest request = new SignupRequest()
            {
                Username = _user.Username,
                Email = "email@email.com",
                Password = _user.Password,
            };

            SignupHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.True(response.ValidationErrors.Any());
            Assert.Null(response.Value);
        }

        [Fact]
        public async void ShouldReturn_Failure_WhenEmailAlreadyExists()
        {
            // Arrange
            SignupRequest request = new SignupRequest()
            {
                Username = "unique.username",
                Email = _user.Email,
                Password = _user.Password,
            };

            SignupHandler handler = new(_mapper, context.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.True(response.ValidationErrors.Any());
            Assert.Null(response.Value);
        }
    }
}

