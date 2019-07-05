using System;
using System.Threading.Tasks;

namespace Sample.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable 
    { 
        Task<string> BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }

    public interface IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        TUnitOfWork Create();
    }
}