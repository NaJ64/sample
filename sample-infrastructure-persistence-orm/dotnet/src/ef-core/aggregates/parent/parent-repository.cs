using ParentAggregate = Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class EFParentRepository : EFRepositoryBase<ParentAggregate.Parent>, ParentAggregate.IParentRepository
    {
        public EFParentRepository(DbSet<ParentAggregate.Parent> dbSet) : base(dbSet) { }

        public override IQueryable<ParentAggregate.Parent> DbSetWithIncludes() => _dbSet.Include(x => x.Children);
    }
}