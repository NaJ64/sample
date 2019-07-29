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
        public static string DEFAULT_SCHEMA => "sample";
        
        public IParentRepository Parents { get; private set; }

        public NHSampleUnitOfWork(ISession session) : base(session) 
        { 
            Parents = new NHParentRepository(session); 
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

        protected override ISampleUnitOfWork CreateInstance(ISession session) => new NHSampleUnitOfWork(session);
    }
}