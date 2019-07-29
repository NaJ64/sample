using System.Threading.Tasks;
using NHibernate.Tool.hbm2ddl;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Infrastructure.Persistence.ORM.NHibernate.Aggregates;

namespace Sample.Tools.Migrations.NHibernate
{
    public class SampleSchemaUpdater
    {
        public static readonly PostgresConnection POSTGRES_CONNECTION = new PostgresConnection()
        {
            Host = "localhost",
            Port = 5432,
            Username = "sample",
            Password = "P0stgr3s",
            Database = "sample-dotnet-nh"
        };
        
        public async Task UpdateDatabaseAsync()
        {
            await new SchemaUpdate(
                new NHSampleUnitOfWorkFactory(POSTGRES_CONNECTION).NHConfiguration
            ).ExecuteAsync(true, true);
        }
    }
}