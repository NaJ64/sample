using System;
using System.Threading.Tasks;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public abstract class EFUnitOfWorkBase<TDbContext> : IUnitOfWork 
        where TDbContext : IDbContext
    {
        protected readonly TDbContext _dbContext;
        private Guid? _transactionId;
        public EFUnitOfWorkBase(TDbContext dbContext) => _dbContext = dbContext;

        public void Dispose() 
        { 
            if (_transactionId.HasValue)
                _dbContext.RollbackTransaction();
            _transactionId = null;
        }

        public async Task<string> BeginAsync() 
        {
            if (_transactionId.HasValue)
                throw new InvalidOperationException("A transaction has already begun");
            var transaction = await _dbContext.BeginTransactionAsync();
            _transactionId = transaction.TransactionId;
            return _transactionId.ToString();
        }

        public async Task CommitAsync()
        {
            if (!_transactionId.HasValue)
                throw new InvalidOperationException("No transaction to commit");
            await _dbContext.SaveTrackedAsync();
            _dbContext.CommitTransaction();
            _transactionId = null;
        }

        public Task RollbackAsync()
        {
            if (!_transactionId.HasValue) 
                throw new InvalidOperationException("No transaction to rollback");
            _dbContext.RollbackTransaction();
            _transactionId = null;
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