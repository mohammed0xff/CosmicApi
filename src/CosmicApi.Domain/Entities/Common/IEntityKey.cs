namespace CosmicApi.Domain.Entities.Common
{
    public interface IEntity
    {
    }
    public interface IEntityKey<out TKey> : IEntity where TKey : notnull
    {
        TKey Id { get; }
    }
}