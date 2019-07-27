using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public abstract class EFRepositoryBase<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregate
    {
        public Task DeleteAsync(int id) => throw new System.NotImplementedException();

        public Task<IEnumerable<TAggregate>> GetAsync() => throw new System.NotImplementedException();

        public Task<IEnumerable<TAggregate>> GetAsync(int skip, int take) => throw new System.NotImplementedException();

        public Task<TAggregate> GetAsync(int id) => throw new System.NotImplementedException();

        public Task<TAggregate> InsertAsync(TAggregate record) => throw new System.NotImplementedException();

        public Task<TAggregate> UpdateAsync(int id, TAggregate record) => throw new System.NotImplementedException();
    }
}