using System;
using System.Threading.Tasks;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public abstract class EFUnitOfWorkBase<TDbContext> : IUnitOfWork 
        where TDbContext : IDbContext
    {
        protected readonly TDbContext _dbContext;
        public EFUnitOfWorkBase(TDbContext dbContext) => _dbContext = dbContext;

        public void Dispose() { }

        public async Task<string> BeginAsync() 
        {
            var transaction = await _dbContext.BeginTransactionAsync();
            return transaction.TransactionId.ToString();
        }

        public Task CommitAsync()
        {
            _dbContext.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task RollbackAsync()
        {
            _dbContext.RollbackTransaction();
            return Task.CompletedTask;
        }
    }

    public abstract class EFUnitOfWorkFactoryBase<TUnitOfWork, TDbContext> : IUnitOfWorkFactory<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
        where TDbContext : IDbContext 
    {
        private readonly Func<TDbContext> _getDbContext;
        public EFUnitOfWorkFactoryBase(Func<TDbContext> getDbContext) => _getDbContext = getDbContext;
        public TUnitOfWork Create() => CreateInstance(_getDbContext());
        protected abstract TUnitOfWork CreateInstance(TDbContext dbContext);
    }
}