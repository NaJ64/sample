using Microsoft.EntityFrameworkCore;
using ParentAggregate = Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions;
using Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates
{
    public interface ISampleDbContext : IDbContext
    {
        DbSet<ParentAggregate.Parent> Parent { get; }
        DbSet<ParentAggregate.Child> Child { get; }
    }

    public class SampleDbContext : DbContextBase, ISampleDbContext
    {
        public DbSet<ParentAggregate.Parent> Parent { get; private set; }
        public DbSet<ParentAggregate.Child> Child { get; private set; }
        public SampleDbContext(PostgresConnection postgres) : base(postgres) { }
        public static string DEFAULT_SCHEMA => "sample";
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
            .ApplyConfiguration(new ParentSchema())
            .ApplyConfiguration(new ChildSchema());
    }
}