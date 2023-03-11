using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CosmicApi.Application.Common.Responses;
using CosmicApi.Application.Features.Users;
using CosmicApi.Application.Features.Users.DeleteUser;
using CosmicApi.Application.Features.Users.GetUserById;
using CosmicApi.Application.Features.Users.GetUsers;
using CosmicApi.Domain.Constants;

namespace CosmicApi.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a paginated response of users.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(PaginatedList<UserResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<UserResponse>>> GetUsers([FromQuery] GetUsersRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var found = await _mediator.Send(new GetUserByIdRequest(id));
            return found == null ? NotFound() : Ok(found);
        }

        /// <summary>
        /// Delete a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _mediator.Send(new DeleteUserRequest(id));
            return deleted ? NotFound() : NoContent();
        }
    }
}
