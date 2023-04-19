using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Users;
using CosmicApi.Application.Features.Users.GetUsers;
using CosmicApi.Domain.Constants;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Pictures.UploadPicture;
using CosmicApi.Configurations.Authentication.ApiKey;

namespace CosmicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        public IMediator _mediator;
        public PictureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("random")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme,
            Roles = Roles.Consumer)]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<UserResponse>>> GetRandomImages()
        {
            return Ok(await _mediator.Send(new GetRandomPicturesRequest()));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = Roles.Admin)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage([FromForm] UploadPictureRequest request)
        {
            var res = await _mediator.Send(request);
            return res.IsSuccess ? 
                Ok(res.Value) : BadRequest(res.Errors);
        }
    }
}
