namespace CosmicApi.Application.Common.Session
{
    public interface ISession
    {
        Guid UserId { get; }
        string Username { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        DateTime Now { get; }
    }
}
