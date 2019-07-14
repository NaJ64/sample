using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Services.DependencyInjection
{
    public static class ServiceCollectionExtensions 
    { 
        public static IServiceCollection AddSampleServices(this IServiceCollection services, Action<IOptions> configureOptions = null) 
        {
            var options = new Options();
            configureOptions?.Invoke(options);
            return services;
        }
    }
}