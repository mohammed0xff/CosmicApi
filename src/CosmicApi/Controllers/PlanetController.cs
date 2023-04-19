using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Pictures;
using CosmicApi.Application.Features.Pictures.GetPictures;
using CosmicApi.Application.Features.Planets;
using CosmicApi.Application.Features.Planets.CreatePlanet;
using CosmicApi.Domain.Constants;
using CosmicApi.Configurations.Authentication.ApiKey;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// Planet Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetController : ControllerBase
    {
        public IMediator _mediator;
        public PlanetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a paginated response of planets.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme, 
            Roles = Roles.Consumer)]
        public async Task<ActionResult<PaginatedList<PlanetResponse>>> Get([FromQuery] GetPlanetRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Get planet by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme,
            Roles = Roles.Consumer)]
        [ProducesResponseType(typeof(PlanetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PlanetResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPlanetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPlanetByIdRequest(id));
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get pictures for a planet by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{id}/pictures")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme,
            Roles = Roles.Consumer)]
        [ProducesResponseType(typeof(PaginatedList<PictureResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Pictures([FromRoute] Guid id, [FromQuery] GetPicturesRequest request)
        {
            return Ok(await _mediator.Send(new GetLuminaryPicturesRequest(request) with { LuminaryId = id }));
        }

        /// <summary>
        /// Create new planet.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = Roles.Admin)]
        [ProducesResponseType(typeof(PlanetResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CreatePlanetRequest request)
        {
            return CreatedAtAction(nameof(Create), await _mediator.Send(request));
        }
    }
}
