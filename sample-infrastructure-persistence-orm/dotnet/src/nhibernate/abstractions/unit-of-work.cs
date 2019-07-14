using System.Threading.Tasks;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public void Dispose() { }

        public Task<string> BeginAsync() => throw new System.NotImplementedException();

        public Task CommitAsync() => throw new System.NotImplementedException();

        public Task RollbackAsync() => throw new System.NotImplementedException();
    }

    public abstract class UnitOfWorkFactoryBase<TUnitOfWork> : IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        public TUnitOfWork Create() => throw new System.NotImplementedException();
    }
}