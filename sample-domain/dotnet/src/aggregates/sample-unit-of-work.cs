using Sample.Domain.Abstractions;
using Sample.Domain.Aggregates.Parent;

namespace Sample.Domain.Aggregates
{
    public interface ISampleUnitOfWork : IUnitOfWork
    {
        IParentRepository Parents { get; }
    }

    public interface ISampleUnitOfWorkFactory : IUnitOfWorkFactory<ISampleUnitOfWork> { }
}