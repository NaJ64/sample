using NHibernate;
using NHibernate.Mapping.ByCode;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates
{
    public class NHSampleUnitOfWork : NHUnitOfWorkBase, ISampleUnitOfWork
    {
        public IParentRepository Parents { get; private set; }

        public NHSampleUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory) 
        { 
            Parents = new NHParentRepository(); 
        }
    }

    public class NHSampleUnitOfWorkFactory : NHUnitOfWorkFactoryBase<ISampleUnitOfWork>, ISampleUnitOfWorkFactory 
    { 
        public NHSampleUnitOfWorkFactory(PostgresConnection postgres) : base(postgres) { }

        protected override void OnEntityMapping(ModelMapper mapper)
        {
            mapper.AddMapping<ParentSchema>();
            mapper.AddMapping<ChildSchema>();
        }

        protected override ISampleUnitOfWork CreateInstance(ISessionFactory sessionFactory) => 
            new NHSampleUnitOfWork(sessionFactory);
    }
}