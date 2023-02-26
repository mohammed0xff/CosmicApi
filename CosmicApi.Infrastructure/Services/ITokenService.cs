﻿using CosmicApi.Domain.Entities;
using CosmicApi.Infrastructure.Common;
using Ardalis.Result;


namespace CosmicApi.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<Jwt> GenerateAccessToken(User user);
        Task<Result<Jwt>> GenerateRefreshToken(string token);
    }
}
