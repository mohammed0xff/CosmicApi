using MediatR;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Galaxies;
using CosmicApi.Application.Features.Galaxies.CreateGalaxy;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Pictures.GetPictures;
using Microsoft.AspNetCore.Authorization;
using CosmicApi.Domain.Constants;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// Galaxy controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GalaxyController : ControllerBase
    {
        public IMediator _mediator;
        public GalaxyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a paginated response of galaxies.
        /// </summary>
        /// <param name="GetGalaxyRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GalaxyResponse>>> Get([FromQuery] GetGalaxyRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Get Galaxy by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetGalaxyById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetGalaxyrByIdRequest(id));
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get pictures for a galaxy by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="GetPicturesRequest"></param>
        /// <returns></returns>
        [HttpGet("{id}/pictures")]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }

        /// <summary>
        /// Create new galaxy.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>201 Created</returns>
        [HttpPost]
        [Authorize(Roles =Roles.Admin)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreateGalaxyRequest request)
        {
            return CreatedAtAction(nameof(Create), await _mediator.Send(request));
        }

        /// <summary>
        /// Delete a galaxy.
        /// </summary>
        /// <param name="DeleteGalaxyRequest"></param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromBody] DeleteGalaxyRequest request)
        {
            return await _mediator.Send(request) ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update a galaxy.
        /// </summary>
        /// <param name="UpdateGalaxyRequest"></param>
        /// <returns>CreatedAtAction</returns>
        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GalaxyResponse), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateGalaxyRequest request)
        {
            var result = await _mediator.Send(request);
            return (result == null) ? NotFound() : CreatedAtAction(nameof(Update), result);
        }

    }
}
