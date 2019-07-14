using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Domain.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleDomain(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);
            return services;
        }
    }
}