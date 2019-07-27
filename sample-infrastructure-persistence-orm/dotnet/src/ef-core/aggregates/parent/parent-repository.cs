using ParentAggregate = Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent
{
    public class EFParentRepository : EFRepositoryBase<ParentAggregate.Parent>, ParentAggregate.IParentRepository { }
}