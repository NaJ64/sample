using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class ChildSchema : ClassMapping<ParentAggregate.Child>
    { 
        public ChildSchema()
        {
			Lazy(false);
			Id(x => x.Id, map => map.Generator(Generators.Native));
			Property(x => x.Id);
            Property(x => x.ParentId);
			Property(x => x.Description);
        }
    }
}