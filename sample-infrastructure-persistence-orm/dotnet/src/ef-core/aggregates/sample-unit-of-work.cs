using System;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Infrastructure.Persistence.ORM.EFCore.Abstractions;
using Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates.Parent;

namespace Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates
{
    public class EFSampleUnitOfWork : EFUnitOfWorkBase<ISampleDbContext>, ISampleUnitOfWork
    {
        public IParentRepository Parents { get; private set; }

        public EFSampleUnitOfWork(ISampleDbContext dbContext) : base(dbContext)
        { 
            Parents = new EFParentRepository(dbContext.Parent); 
        }
    }

    public class EFSampleUnitOfWorkFactory : EFUnitOfWorkFactoryBase<ISampleUnitOfWork, ISampleDbContext>, ISampleUnitOfWorkFactory 
    { 
        public EFSampleUnitOfWorkFactory(Func<ISampleDbContext> getDbContext) : base(getDbContext) { }

        protected override ISampleUnitOfWork CreateInstance(ISampleDbContext dbContext) => new EFSampleUnitOfWork(dbContext);
    }
}