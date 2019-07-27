using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions;
using Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates
{
    public class EFSampleUnitOfWork : EFUnitOfWorkBase, ISampleUnitOfWork
    {
        public IParentRepository Parents { get; private set; }

        public EFSampleUnitOfWork()
        { 
            Parents = new EFParentRepository(); 
        }
    }

    public class EFSampleUnitOfWorkFactory : EFUnitOfWorkFactoryBase<ISampleUnitOfWork>, ISampleUnitOfWorkFactory 
    { 
        public EFSampleUnitOfWorkFactory(PostgresConnection postgres) : base(postgres) { }
    }
}