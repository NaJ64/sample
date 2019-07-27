namespace Sample.Domain.Abstractions
{
    public interface IEntity : IEntity<int> { }
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
    public abstract class EntityBase : IEntity<int>
    {
        public virtual int Id { get; set; }
    }
}