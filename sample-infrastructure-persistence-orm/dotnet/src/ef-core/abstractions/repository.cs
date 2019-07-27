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
        private readonly DbSet<TAggregate> _dbSet;

        public EFRepositoryBase(DbSet<TAggregate> dbSet) => _dbSet = dbSet;

        public Task DeleteAsync(int id) => throw new System.NotImplementedException();

        public async Task<IEnumerable<TAggregate>> GetAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TAggregate>> GetAsync(int skip, int take) => await _dbSet.AsQueryable()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        public async Task<TAggregate> GetAsync(int id) => await _dbSet.FindAsync(new [] { id });

        public async Task<TAggregate> InsertAsync(TAggregate record) => (await _dbSet.AddAsync(record))?.Entity;

        public Task<TAggregate> UpdateAsync(int id, TAggregate record) => Task.FromResult( _dbSet.Update(record)?.Entity);
    }
}