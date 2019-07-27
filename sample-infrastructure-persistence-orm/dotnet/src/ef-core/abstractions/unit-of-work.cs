using System.Threading.Tasks;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public abstract class EFUnitOfWorkBase : IUnitOfWork
    {
        public void Dispose() { }

        public Task<string> BeginAsync() => throw new System.NotImplementedException();

        public Task CommitAsync() => throw new System.NotImplementedException();

        public Task RollbackAsync() => throw new System.NotImplementedException();
    }

    public abstract class EFUnitOfWorkFactoryBase<TUnitOfWork> : IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {        
        public EFUnitOfWorkFactoryBase(PostgresConnection postgres) { }

        public TUnitOfWork Create() => throw new System.NotImplementedException();
    }
}