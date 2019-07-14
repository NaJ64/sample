using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Aggregates;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates;

namespace Sample.Infrastructure.Persistence.ORM.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleOrmPersistence(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);

            if (!options.Postgres.HasValue)
                throw new ArgumentNullException("PostgreSQL connections options must be provided");

            // ISampleUnitOfWorkFactory
            services.AddSingleton<ISampleUnitOfWorkFactory, SampleUnitOfWorkFactory>();

            return services;
        }
    }
}