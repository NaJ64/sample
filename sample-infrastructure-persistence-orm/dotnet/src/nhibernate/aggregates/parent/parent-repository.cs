using ParentAggregate = Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class ParentRepository : RepositoryBase<ParentAggregate.Parent>, ParentAggregate.IParentRepository { }
}