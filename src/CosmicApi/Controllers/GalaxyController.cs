using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Galaxies;
using CosmicApi.Application.Features.Galaxies.CreateGalaxy;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Pictures.GetPictures;


namespace CosmicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalaxyController : ControllerBase
    {
        public IMediator _mediator;
        public GalaxyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<GalaxyResponse>>> Get([FromQuery] GetGalaxyRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetGalaxyById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetGalaxyrByIdRequest(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id}/pictures")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateGalaxyRequest request)
        {
            return CreatedAtAction(nameof(Create), await _mediator.Send(request));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromBody] DeleteGalaxyRequest request)
        {
            return (await _mediator.Send(request) == null) ? NoContent() : NotFound();
        }

        [HttpPut]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateGalaxyRequest request)
        {
            var result = await _mediator.Send(request);
            return (result == null) ? NotFound() : CreatedAtAction(nameof(Update), result);
        }

    }
}
