using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Stars;
using CosmicApi.Application.Features.Stars.GetStar;
using CosmicApi.Application.Features.Stars.CreateStar;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// Star controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StarController : ControllerBase
    {
        public IMediator _mediator;
        public StarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a paginated response of stars.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<StarResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<StarResponse>>> Get([FromQuery] GetStarRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Get star by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StarResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStarById(Guid id)
        {
            var result = await _mediator.Send(new GetStarByIdRequest(id));
            return (result == null) ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Create new star.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create([FromBody] CreateStarRequest request)
        {
            return CreatedAtAction(nameof(GetStarById), await _mediator.Send(request));

        }

        /// <summary>
        /// Get pictures for a star by id.
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
    }
}
