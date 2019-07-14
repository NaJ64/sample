using System;
using Sample.Domain.DependencyInjection;
using Sample.Infrastructure.Services.Domain.Commands;
using Sample.Infrastructure.Services.Domain.Queries;
using Sample.Services.Commands;
using Sample.Services.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Infrastructure.Services.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleDomainServices(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);

            if (options.EnableCommands || options.EnableQueries)
            {
                services.AddSampleDomain();
            }

            if (options.EnableCommands)
            {
                services.AddTransient<IAddSomethingHandler, DomainAddSomethingHandler>();
                services.AddTransient<IUpdateSomethingHandler, DomainUpdateSomethingHandler>();
                services.AddTransient<IRemoveSomethingHandler, DomainRemoveSomethingHandler>();
            }

            if (options.EnableQueries)
            {
                services.AddTransient<IQueries, DomainQueries>();
            }

            return services;
        }
    }
}