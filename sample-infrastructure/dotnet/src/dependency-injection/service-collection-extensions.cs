using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Infrastructure.Persistence.ORM.DependencyInjection;
using Sample.Infrastructure.Services.Domain.DependencyInjection;

namespace Sample.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleInfrastructure(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);

            if (options.DomainCommands || options.DomainQueries) 
            {
                services.AddSampleDomainServices(domainServices => 
                {
                    domainServices.EnableCommands = options.DomainCommands;
                    domainServices.EnableQueries = options.DomainQueries;
                });
            }

            if (options.Postgres != null) 
            {
                services.AddSampleOrmPersistence(ormPersistence => 
                {
                    ormPersistence.Postgres = options.Postgres;
                    ormPersistence.ORM = options.ORM ?? OrmType.NHibernate;
                });
            }

            return services;
        }
    }
}