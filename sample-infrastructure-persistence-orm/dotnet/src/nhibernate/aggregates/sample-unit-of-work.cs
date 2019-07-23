using NHibernate;
using NHibernate.Mapping.ByCode;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.DependencyInjection;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates
{
    public class SampleUnitOfWork : UnitOfWorkBase, ISampleUnitOfWork
    {
        public IParentRepository Parents { get; private set; }

        public SampleUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory) 
        { 
            Parents = new ParentRepository(); 
        }
    }

    public class SampleUnitOfWorkFactory : UnitOfWorkFactoryBase<ISampleUnitOfWork>, ISampleUnitOfWorkFactory 
    { 
        public SampleUnitOfWorkFactory(IOptions options) : base(options) { }

        protected override void OnEntityMapping(ModelMapper mapper)
        {
            mapper.AddMapping<ParentSchema>();
            mapper.AddMapping<ChildSchema>();
        }

        protected override ISampleUnitOfWork CreateInstance(ISessionFactory sessionFactory) => 
            new SampleUnitOfWork(sessionFactory);
    }
}