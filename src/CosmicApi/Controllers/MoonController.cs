using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Moons;
using CosmicApi.Application.Features.Moons.GetMoonById;
using CosmicApi.Application.Features.Moons.GetMoon;
using CosmicApi.Application.Features.Moons.CreateMoon;

namespace CosmicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoonController : ControllerBase
    {
        public IMediator _mediator;
        public MoonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<MoonResponse>>> Get([FromQuery] GetMoonRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMoonById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetMoonByIdRequest(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id}/pictures")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MoonResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateMoonRequest request)
        {
            return CreatedAtAction(nameof(Create), await _mediator.Send(request));
        }

    }
}
