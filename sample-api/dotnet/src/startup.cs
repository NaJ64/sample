using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sample.API.DependencyInjection;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Services.Commands;
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

            app.Map("/favicon.ico", delegate { }); // Ignore favicon request(s)

            app.Run(async (context) =>
            {
                try 
                {

                    var queries = app.ApplicationServices.GetRequiredService<IQueries>();
                    var records = await queries.GetSomethingsAsync();
                    if (records.Any())
                    {
                        var firstSomething = records.First();

                        var updateSomethingHandler = app.ApplicationServices.GetRequiredService<IUpdateSomethingHandler>();
                        await updateSomethingHandler.HandleAsync(new UpdateSomethingCommand
                        {
                            SomeId = firstSomething.SomeId,
                            SomeNewData = firstSomething.SomeData + " | " + DateTime.UtcNow.ToShortTimeString()
                        });
                        
                        // var removeSomethingHandler = app.ApplicationServices.GetRequiredService<IRemoveSomethingHandler>();
                        // await removeSomethingHandler.HandleAsync(new RemoveSomethingCommand
                        // {
                        //     SomeId = firstSomething.SomeId
                        // });
                    } 
                    else
                    {
                        var addSomethingHandler = app.ApplicationServices.GetRequiredService<IAddSomethingHandler>();
                        await addSomethingHandler.HandleAsync(new AddSomethingCommand
                        {
                            SomeNewData = DateTime.UtcNow.ToShortTimeString()
                        });
                    }
                    
                    records = await queries.GetSomethingsAsync();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(records));
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("Encountered error: {0}\n\n{1}", 
                        JsonConvert.SerializeObject(e.Message), 
                        JsonConvert.SerializeObject(e.StackTrace)
                    );
                    await context.Response.WriteAsync(string.Format("{0}\n\n{1}", 
                        JsonConvert.SerializeObject(e.Message), 
                        JsonConvert.SerializeObject(e.StackTrace)
                    ));
                }
            });
        }
    }
}
