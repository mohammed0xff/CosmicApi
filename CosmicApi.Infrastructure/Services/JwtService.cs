using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Common;
using CosmicApi.Infrastructure.Context;
using Ardalis.Result;


namespace CosmicApi.Infrastructure.Services
{
    public class JwtService : ITokenService
    {
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IContext _context;

        public JwtService(
            TokenValidationParameters tokenValidationParams,
            IOptions<TokenConfiguration> tokenConfiguration,
            IContext context
            )
        {
            _tokenConfiguration = tokenConfiguration.Value;
            _tokenValidationParams = tokenValidationParams;
            _context = context;
        }

        public async Task<Jwt> GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            });

            var expiryDate = DateTime.UtcNow
                .AddHours(_tokenConfiguration.DurationInDays);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _tokenConfiguration.Audience,
                Issuer = _tokenConfiguration.Issuer,
                Subject = claims,
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    _tokenConfiguration.Algorithm
                    ),
            };

            // create token 
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // create refresh token
            var refreshToken = new RefreshToken()
            {
                UserId = user.Id.ToString(),
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            // save refresh token 
            await _context.Tokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            
            return new Jwt
            {
                Token = tokenHandler.WriteToken(token),
                ExpDate = expiryDate,
                RefreshToken = refreshToken.Token
            };
        }


        public async Task<Result<Jwt>> GenerateRefreshToken(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // jwt format validation 
                _tokenValidationParams.ValidateLifetime = false;
                var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);
                _tokenValidationParams.ValidateLifetime = true;

                // encryption algorithm validate 
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(_tokenConfiguration.Algorithm, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                        return Result.Error("Please try to login again");
                }

                // expiry date validation
                var utcExpiryDate = long.Parse(
                    tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value
                    );

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate < DateTime.UtcNow)
                {
                    return Result.Error("Refresh token has expired");
                }

                // token existence validation
                var storedToken = await _context.Tokens
                    .FirstOrDefaultAsync(x => x.Token == token);

                if (storedToken == null)
                {
                    return Result.Error("Token does not exist");
                }

                // update current token 
                _context.Tokens.Remove(storedToken);
                await _context.SaveChangesAsync();

                // Generate a new token
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id.ToString() == storedToken.UserId);
                
                return Result.Success(await GenerateAccessToken(user!));
            }
            catch (Exception ex)
            {
                return Result.Error($"Failed to refresh token, {ex.Message}");
            }
        }


        // https://stackoverflow.com/a/250400
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime;
        }


        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable
                .Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)])
                .ToArray()
                );
        }

    }

}
