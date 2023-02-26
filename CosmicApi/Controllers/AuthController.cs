using Microsoft.AspNetCore.Mvc;
using MediatR;
using CosmicApi.Application.Features.Auth.Authenticate;
using CosmicApi.Application.Features.Auth.RefreshToken;
using CosmicApi.Application.Features.Auth.Signup;
using CosmicApi.Extensions;

namespace CosmicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
            => (await _mediator.Send(request)).ToActionResult();

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
            => (await _mediator.Send(request)).ToActionResult();

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
            => (await _mediator.Send(request)).ToActionResult();

    }
}
