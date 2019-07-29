using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using ParentAggregate = Sample.Domain.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent
{
    public class ParentSchema : ClassMapping<ParentAggregate.Parent> 
    { 
        public ParentSchema()
        {
            Schema(NHSampleUnitOfWork.DEFAULT_SCHEMA);
            Table(nameof(ParentAggregate.Parent).ToLowerInvariant());
			Lazy(false);
            Id(p => p.Id, map => map.Generator(Generators.Increment));
            Property(p => p.Description, map => map.Column(nameof(ParentAggregate.Parent.Description).ToLower()));
            Bag(
                p => p.Children, 
                c => {
                    c.Inverse(false);
                    c.Lazy(CollectionLazy.NoLazy);
                    c.Key(k => {
                        k.Column(nameof(ParentAggregate.Child.ParentId).ToLowerInvariant());
                        k.NotNullable(true);
                    });
                    c.Cascade(Cascade.All);
                }, 
                map => map.OneToMany(p => p.Class(typeof(ParentAggregate.Child)))
            );
        }
    }
}