using Ardalis.Result;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicApi.Application.Features.Auth.RefreshToken
{
    public class RefreshTokenRequest :IRequest<Result<Jwt>>
    {
        public RefreshTokenRequest(string token) => Token = token;

        [Required]
        public string Token { get; }

    }
}
