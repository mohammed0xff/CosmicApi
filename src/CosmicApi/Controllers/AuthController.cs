using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Features.Auth.Authenticate;
using CosmicApi.Application.Features.Auth.RefreshToken;
using CosmicApi.Application.Features.Auth.Signup;
using CosmicApi.Application.Extensions;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        public IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login an existing user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult?> Login([FromBody] LoginRequest request)
            => (await _mediator.Send(request)).ToActionResult();

        /// <summary>
        /// Signup new user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Signup")]
        public async Task<IActionResult?> Signup([FromBody] SignupRequest request)
            => (await _mediator.Send(request)).ToActionResult();

        /// <summary>
        /// Refresh an expired token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public async Task<IActionResult?> RefreshToken([FromBody] RefreshTokenRequest request)
            => (await _mediator.Send(request)).ToActionResult();

    }
}
