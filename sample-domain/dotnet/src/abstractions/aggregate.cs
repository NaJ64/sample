namespace Sample.Domain.Abstractions
{
    public interface IAggregate : IAggregate<int> { }
    public interface IAggregate<TKey> : IEntity<TKey> { }
    public abstract class AggregateBase : EntityBase, IAggregate<int> { }
}