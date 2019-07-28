using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Domain.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public abstract class EFRepositoryBase<TAggregate> : IRepository<TAggregate> where TAggregate : class, IAggregate
    {
        protected readonly DbSet<TAggregate> _dbSet;

        public EFRepositoryBase(DbSet<TAggregate> dbSet) => _dbSet = dbSet;

        public abstract IQueryable<TAggregate> DbSetWithIncludes();

        public async Task DeleteAsync(int id) => _dbSet.Remove(await _dbSet.FindAsync(id));

        public async Task<IEnumerable<TAggregate>> GetAsync() => await DbSetWithIncludes()
            .ToListAsync();

        public async Task<IEnumerable<TAggregate>> GetAsync(int skip, int take) => await DbSetWithIncludes()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<TAggregate> GetAsync(int id) => await DbSetWithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TAggregate> InsertAsync(TAggregate record) => (await _dbSet.AddAsync(record))?.Entity;

        public Task<TAggregate> UpdateAsync(int id, TAggregate record) => Task.FromResult( _dbSet.Update(record)?.Entity);
    }
}