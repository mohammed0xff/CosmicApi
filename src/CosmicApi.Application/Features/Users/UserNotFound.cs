namespace CosmicApi.Application.Features.Users
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(Guid userId)
			: base($"User with id '{userId}' was not found.")
		{
		}
	}
}