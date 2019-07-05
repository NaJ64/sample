using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Domain.Abstractions 
{
    public interface IRepository<TAggregate> : IRepository<TAggregate, int> where TAggregate : IAggregate { }
    public interface IRepository<TAggregate, TKey> where TAggregate : IAggregate<TKey> 
    {
        Task<IEnumerable<TAggregate>> GetAsync();
        Task<IEnumerable<TAggregate>> GetAsync(int skip, int take);
        Task<TAggregate> GetAsync(TKey id);
        Task<TAggregate> InsertAsync(TAggregate record);
        Task<TAggregate> UpdateAsync(TKey id, TAggregate record);
        Task DeleteAsync(TKey id);
    }
}