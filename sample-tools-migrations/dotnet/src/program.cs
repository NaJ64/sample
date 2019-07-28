using System;
using System.Linq;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Tools.Migrations.NHibernate;

namespace Sample.Tools.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any()) 
            {
                WriteLineCyan("Encountered error: No argument(s) were provided\n");
                WriteLineCyan("Specify 'nh' for NHibernate or 'ef' for EFCore commands");
                return;
            }

            OrmType orm = args[0].Equals("nh", StringComparison.OrdinalIgnoreCase)
                ? OrmType.NHibernate
                : args[0].Equals("ef", StringComparison.OrdinalIgnoreCase)
                    ? OrmType.EFCore
                    : default(OrmType);
            
            if (orm == default(OrmType))
            {
                WriteLineCyan("Encountered error:  Unrecognized argument(s)\n");
                return;
            }

            if (orm == OrmType.EFCore)
            {
                WriteLineCyan("EF Core migrations must be performed using the dotnet CLI\n");
                WriteLineCyan("Creating a new migration:  \n");
                WriteLineCyan("    dotnet ef migrations add <MIGRATION_NAME>\n");
                WriteLineCyan("Applying migration(s):  \n");
                WriteLineCyan("    dotnet ef database update");
                return;
            }

            if (orm == OrmType.NHibernate)
            {
                if (args.Length < 2)
                {
                    WriteLineCyan("Encountered error:  No NHibernate argument(s) were provided\n");
                    WriteLineCyan("Specify 'nh update' to run schema update against the database");
                    return;
                }
                var subArg = args[1];
                if (!subArg.Equals("update", StringComparison.OrdinalIgnoreCase))
                {
                    WriteLineCyan("Encountered error:  Unrecognized NHibernate argument(s)");
                    return;
                }
                WriteLineCyan($"Updating database '{SampleSchemaUpdater.POSTGRES_CONNECTION.Database}...'\n");
                new SampleSchemaUpdater()
                    .UpdateDatabaseAsync()
                    .GetAwaiter()
                    .GetResult();
                WriteLineCyan("Finished!");
            }
        }

        static void WriteLineCyan(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
