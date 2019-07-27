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

            services.AddSampleInfrastructure(infrastruture =>
            {
                infrastruture.DomainCommands = options.DomainCommands;
                infrastruture.DomainQueries = options.DomainQueries;
                infrastruture.ORM = OrmType.NHibernate;
                infrastruture.Postgres = options.Postgres;
            });
            
            return services;
        }
    }
}