using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infrastructure.DependencyInjection;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Services.DependencyInjection;

namespace Sample.API.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleApi(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);

            services.AddSampleServices();

            services.AddSampleInfrastructure(infrastructure =>
            {
                infrastructure.DomainCommands = options.DomainCommands;
                infrastructure.DomainQueries = options.DomainQueries;
                infrastructure.ORM = OrmType.NHibernate;
                infrastructure.Postgres = options.Postgres;
            });
            
            return services;
        }
    }
}