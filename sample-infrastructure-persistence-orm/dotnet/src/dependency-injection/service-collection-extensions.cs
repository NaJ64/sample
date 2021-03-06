using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Aggregates;
using Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates;

namespace Sample.Infrastructure.Persistence.ORM.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleOrmPersistence(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);

            if (options.Postgres == null)
                throw new ArgumentNullException(nameof(options.Postgres), "PostgreSQL connections options must be provided");
            if (options.ORM.Equals(default(OrmType)))
                throw new ArgumentNullException(nameof(options.ORM), "ORM specification must be provided");

            // ISampleUnitOfWorkFactory
            if (options.ORM == OrmType.EFCore)
            {
                services.AddTransient<ISampleDbContext, SampleDbContext>(sp => new SampleDbContext(options.Postgres));
                services.AddSingleton<ISampleUnitOfWorkFactory, EFSampleUnitOfWorkFactory>(sp =>
                    new EFSampleUnitOfWorkFactory(() => sp.GetRequiredService(typeof(ISampleDbContext)) as ISampleDbContext)
                );
            }
            if (options.ORM == OrmType.NHibernate)
                services.AddSingleton<ISampleUnitOfWorkFactory, NHSampleUnitOfWorkFactory>(sp => 
                    new NHSampleUnitOfWorkFactory(options.Postgres));

            return services;
        }
    }
}