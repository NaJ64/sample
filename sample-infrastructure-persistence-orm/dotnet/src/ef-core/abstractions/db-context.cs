using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class; // Matches existing method signature on base DbContext
        EntityEntry Entry(object entry); // Matches existing method signature on base DbContext
        Task<IReadOnlyCollection<EntityEntry>> SaveTrackedAsync(); 
        void ResetTracked();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void RollbackTransaction();
        void CommitTransaction();
        IReadOnlyCollection<EntityEntry> GetTracked();
    }

    public abstract class DbContextBase : DbContext, IDbContext
    {
        protected readonly PostgresConnection _postgres;

        public DbContextBase(PostgresConnection postgres) : base() => _postgres = postgres;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            // Get the sql server connection string from options
            var connectionString = _postgres.ToConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(_postgres), "No connection string was provided via DbContext constructor");
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptionsBuilder => OnNpgsqlConfiguring(npgsqlOptionsBuilder));
        }

        protected virtual void OnNpgsqlConfiguring(NpgsqlDbContextOptionsBuilder npgsqlOptionsBuilder) { }

        public async Task<IReadOnlyCollection<EntityEntry>> SaveTrackedAsync()
        {
            // Collect all the entities from the change tracker
            var trackedEntities = GetTracked();
            // Commit changes on base DbContext
            await base.SaveChangesAsync();
            // Return the collection of entities
            return trackedEntities;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() => base.Database.BeginTransactionAsync();

        public void CommitTransaction() => base.Database.CommitTransaction();

        public void RollbackTransaction() => base.Database.RollbackTransaction();

        public IReadOnlyCollection<EntityEntry> GetTracked() => base.ChangeTracker
            .Entries()
            .Where(entry => entry?.Entity != null)
            .ToList()
            .AsReadOnly();

        public void ResetTracked() => base.ChangeTracker
            .Entries()
            .Where(entry => entry?.Entity != null)
            .ToList()
            .ForEach(x => x.State = EntityState.Detached);

    }
}