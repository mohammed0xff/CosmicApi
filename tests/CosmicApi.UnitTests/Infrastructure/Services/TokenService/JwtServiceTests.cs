using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Moq;
using MockQueryable.Moq;
using Xunit;
using Xunit.Categories;

using CosmicApi.Domain.Constants;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Common;
using CosmicApi.Infrastructure.Context;
using CosmicApi.Infrastructure.Services.TokenService;

namespace CosmicApi.UnitTests.Infrastructure.Services.TokenService
{
    [UnitTest]
    public class JwtServiceTests
    {
        private TokenConfiguration tokenConfiguration;
        private Mock<IContext> context = new();
        private User user = new();
        private List<User> usersList = new();
        private RefreshToken refreshToken = new();

        public JwtServiceTests()
        {
            tokenConfiguration = CreateTokenConfigurarion();
            
            // user
            user.Role = Roles.User;
            user.Username = "username";
            user.Email = "email@mail.com";
            usersList.Add(user);

            // refresh token
            refreshToken.Token = "123456789";
            refreshToken.UserId = user.Id;
            refreshToken.AddedDate = DateTime.UtcNow;
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            // refresh token dbset
            var refreshTokenList = new List<RefreshToken> { refreshToken };
            var MocDbsetRefreshTokens = refreshTokenList.AsQueryable().BuildMockDbSet();

            // users dbset
            var mockDbsetUsers = usersList.AsQueryable().BuildMockDbSet();

            // setup context
            context.Setup(x => x.Tokens).Returns(MocDbsetRefreshTokens.Object);
            context.Setup(x => x.Users).Returns(mockDbsetUsers.Object);
        }

        [Fact]
        public async void Should_ReturnAcessToken_WhenUserNotNull()
        {
            // Arrange
            var service = CreateJwtService();

            // Act
            RefreshTokenResponse jwt = await service.GenerateAccessToken(user);
            
            // Assert
            Assert.NotNull( jwt );
            Assert.NotNull( jwt.Token );
            Assert.NotNull( jwt.RefreshToken );
            var expectedExpireTime = DateTime.UtcNow
                .AddMinutes(tokenConfiguration.DurationInMinutes);
            Assert.Equal( jwt.ExpDate.Hour, expectedExpireTime.Hour);
            Assert.Equal( jwt.ExpDate.Minute, expectedExpireTime.Minute);
        }
/*
        [Fact]
        public async void Should_GenerateRefreshToken_WhenTokenIsValid()
        {
            // Arrange

            // create service
            var service = CreateJwtService();
            
            // Act
            var result = await service.RefreshToken(refreshToken.Token);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Token);
            Assert.NotNull(result.Value.RefreshToken);

        }

        [Fact]
        public async void Should_ReturnError_WhenTokenAlreadyExpired()
        {
            // Arrange
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(-1);
            
            // create service
            var service = CreateJwtService();

            // Act
            var result = await service.RefreshToken(refreshToken.Token);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.True(result.Errors.Any());
            Assert.Null(result.Value);
        }

        [Theory]
        [InlineData(null)]
        public async void Should_ReturnError_WhenTokenNotFound(string token)
        {
            // Arrange
            // create service
            var service = CreateJwtService();

            // Act
            var result = await service.RefreshToken(token);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.True(result.Errors.Any());
            Assert.Null(result.Value);
        }
*/
        private TokenConfiguration CreateTokenConfigurarion()
        {
            return new TokenConfiguration
            {
                Secret = "highSecret123456789asdfjkl;asdfjkl;",
                Algorithm = "HS256",
                Audience = "audience123",
                Issuer = "issuer123",
                DurationInMinutes = 20
            };
        }

        private JwtService CreateJwtService()
        {
            // token params
            var tokenValidationParams = new Mock<TokenValidationParameters>();
            // token options
            var tokenConfigurationOptions = Options.Create<TokenConfiguration>(tokenConfiguration);

            // create service
            return new JwtService(
                tokenValidationParams.Object,
                tokenConfigurationOptions,
                context.Object
                );
        }

    }
}
