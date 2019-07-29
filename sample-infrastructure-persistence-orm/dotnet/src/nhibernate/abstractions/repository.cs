using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions
{
    public abstract class NHRepositoryBase<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregate
    {

        private readonly ISession _session;

        public NHRepositoryBase(ISession session) => _session = session;

        protected string AggregateName() => nameof(TAggregate);

        public async Task DeleteAsync(int id) => await _session.DeleteAsync(await GetAsync(id));

        public async Task<IEnumerable<TAggregate>> GetAsync() => await _session.Query<TAggregate>().ToListAsync();

        public async Task<IEnumerable<TAggregate>> GetAsync(int skip, int take) => await _session.Query<TAggregate>()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<TAggregate> GetAsync(int id) => await _session.GetAsync<TAggregate>(id);

        public async Task<TAggregate> InsertAsync(TAggregate record)
        {
            var inserted = await _session.SaveAsync(record);
            return await _session.GetAsync<TAggregate>(inserted);
        }

        public async Task<TAggregate> UpdateAsync(int id, TAggregate record) 
        {
            await _session.UpdateAsync(record);
            return record;
        }
    }
}