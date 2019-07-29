using NHibernate;
using ParentAggregate = Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class NHParentRepository : NHRepositoryBase<ParentAggregate.Parent>, ParentAggregate.IParentRepository 
    { 
        public NHParentRepository(ISession session) : base(session) { }
    }
}