using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class ChildSchema : ClassMapping<ParentAggregate.Child>
    { 
        public ChildSchema()
        {
            Schema(NHSampleUnitOfWork.DEFAULT_SCHEMA);
            Table(nameof(ParentAggregate.Child).ToLowerInvariant());
			Lazy(false);
			Id(c => c.Id, map => map.Generator(Generators.Increment));
			Property(c => c.Description);
            Property(c => c.ParentId);
        }
    }
}