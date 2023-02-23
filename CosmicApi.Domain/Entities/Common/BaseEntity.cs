namespace CosmicApi.Domain.Entities.Common
{
    public abstract class BaseEntity : IEntityKey<Guid>
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
    }
}