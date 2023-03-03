using CosmicApi.Application.Common.Responses;

namespace CosmicApi.Application.Features.Users;

public record UserNotFound : NotFound {}

public class UserNotFoundException : Exception {
	public UserNotFoundException(Guid userId) 
		: base($"User with id '{userId}' not found.")
	{
	}

}