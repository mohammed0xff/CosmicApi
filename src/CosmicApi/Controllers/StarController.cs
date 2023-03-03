using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Stars;
using CosmicApi.Application.Features.Stars.GetStar;
using CosmicApi.Application.Features.Stars.CreateStar;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Pictures;

namespace CosmicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        public IMediator _mediator;
        public StarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<StarResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<StarResponse>>> Get([FromQuery] GetStarRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StarResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetStarById(Guid id)
        {
            var result = await _mediator.Send(new GetStarByIdRequest(id));
            return (result == null) ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateStarRequest request)
        {
            return CreatedAtAction(nameof(GetStarById), await _mediator.Send(request));

        }

        [HttpGet("{id}/pictures")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }
    }
}
