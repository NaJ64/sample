using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Abstractions;

namespace Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates
{
    public class SampleUnitOfWork : UnitOfWorkBase, ISampleUnitOfWork
    {
        public IParentRepository Parents => throw new System.NotImplementedException();
    }

    public class SampleUnitOfWorkFactory : UnitOfWorkFactoryBase<ISampleUnitOfWork>, ISampleUnitOfWorkFactory { }
}