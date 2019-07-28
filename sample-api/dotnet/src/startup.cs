using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sample.API.DependencyInjection;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Services.Queries;

namespace Sample.API
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var postgres = new PostgresConnection();
            _configuration.Bind("postgres", postgres);
            var ormType = OrmType.FromName(_configuration.GetValue<string>("orm"));

            services.AddSampleApi(options => 
            {
                options.DomainCommands = true;
                options.DomainQueries = true;
                options.ORM = ormType;
                options.Postgres = postgres;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                try 
                {

                    var queries = app.ApplicationServices.GetRequiredService<IQueries>();
                    var records = await queries.GetSomethingsAsync();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(records));
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("Encountered error: {0}", JsonConvert.SerializeObject(e));
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(e));
                }
            });
        }
    }
}
