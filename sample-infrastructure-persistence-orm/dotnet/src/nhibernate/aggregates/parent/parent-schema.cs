using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class ParentSchema : ClassMapping<ParentAggregate.Parent> 
    { 
        public ParentSchema()
        {
            Lazy(false);
			Id(x => x.Id, map => map.Generator(Generators.Native));
			Property(x => x.Id);
			Property(x => x.Description);
            Set(x => x.Children, map => 
            {
                map.Lazy(CollectionLazy.NoLazy);
                map.Key(k =>
                {
                    k.Column(nameof(ParentAggregate.Child.ParentId));
                    k.NotNullable(true);
                });
            });
        }
    }
}