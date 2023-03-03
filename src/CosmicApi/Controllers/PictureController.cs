using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Users;
using CosmicApi.Application.Features.Users.GetUsers;
using CosmicApi.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CosmicApi.Application.Features.Pictures.GetPictures;

namespace CosmicApi.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        public IMediator _mediator;
        public PictureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("/random")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<UserResponse>>> GetRandomImages()
        {
            return Ok(await _mediator.Send(new GetRandomPicturesRequest()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage([FromBody] IFormFile file)
        {
            var res = await _mediator.Send(file);
            return res == null ? 
                Ok(res) : BadRequest("Please try again.");
        }
    }
}
