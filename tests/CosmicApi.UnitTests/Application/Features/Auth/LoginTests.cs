using AutoMapper;
using BC = BCrypt.Net.BCrypt;

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
using CosmicApi.UnitTests.Application.Helpers;

namespace CosmicApi.UnitTests.Application.Features.LoginTests
{

    [UnitTest]
    public class LoginTests : FeatureTests
    {
        private IMapper _mapper;
        private List<User> _sourceUsers = new();
        private User _user = new();
        private Mock<IContext> context = new Mock<IContext>();
        private Mock<ITokenService> _tokenService = new();
        public LoginTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _user.Email = "email@email.com";
            _user.Password = "password";

            _sourceUsers.Add(
                _user
                );
            var mockDbsetUsers = _sourceUsers.AsQueryable().BuildMockDbSet();

            context.Setup(x => x.Users).Returns(mockDbsetUsers.Object);

            _tokenService.Setup(x => x.GenerateAccessToken(It.IsAny<User>())).ReturnsAsync(
                new RefreshTokenResponse() { Token = "", ExpDate = DateTime.UtcNow, RefreshToken = "" }
                );

        }

        // Invalid salt version!
        [Fact]
        public async void ShouldReturn_Success_WhenValidCredentials()
        {         
            // Arrange
            LoginRequest request = new LoginRequest()
            {
                Password = BC.HashPassword(_user.Password,BC.GenerateSalt()),
                Email = _user.Email
            };

            LoginHandler handler = new(context.Object, _tokenService.Object);

            // Act 
            var response = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.False(response.Errors.Any());
            Assert.NotNull(response.Value);
            Assert.NotNull(response.Value.Token);
        }
    }
}

