using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Moons;
using CosmicApi.Application.Features.Moons.GetMoonById;
using CosmicApi.Application.Features.Moons.GetMoon;
using CosmicApi.Application.Features.Moons.CreateMoon;
using CosmicApi.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// Moon controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MoonController : ControllerBase
    {
        public IMediator _mediator;
        public MoonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a paginated response of moons.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<MoonResponse>>> Get([FromQuery] GetMoonRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Get moon by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMoonById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetMoonByIdRequest(id));
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get pictures for a moon by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{id}/pictures")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }

        /// <summary>
        /// Create a new moon.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateMoonRequest request)
        {
            return CreatedAtAction(nameof(Create), await _mediator.Send(request));
        }

    }
}
