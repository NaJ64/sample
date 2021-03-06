using System;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Sample.Infrastructure.Persistence.ORM;
using Sample.Infrastructure.Persistence.ORM.EFCore.Aggregates;

namespace Sample.Tools.Migrations.EFCore
{
    public class SampleMigrationsDbContext : SampleDbContext
    {
        private readonly Action<NpgsqlDbContextOptionsBuilder> _onNpgsqlConfiguring;
        public SampleMigrationsDbContext(PostgresConnection postgres, Action<NpgsqlDbContextOptionsBuilder> onNpgsqlConfiguring = null) : base(postgres) 
        { 
            _onNpgsqlConfiguring = onNpgsqlConfiguring;
        }
        protected override void OnNpgsqlConfiguring(NpgsqlDbContextOptionsBuilder npgsqlOptionsBuilder)=> 
            _onNpgsqlConfiguring?.Invoke(npgsqlOptionsBuilder);
    }

    public class SampleMigrationsDbContextFactory : IDesignTimeDbContextFactory<SampleMigrationsDbContext>
    {
        public static readonly PostgresConnection POSTGRES_CONNECTION = new PostgresConnection()
        {
            Host = "localhost",
            Port = 5432,
            Username = "sample",
            Password = "P0stgr3s",
            Database = "sample-dotnet-ef"
        };

        public SampleMigrationsDbContext CreateDbContext(string[] args) =>
            new SampleMigrationsDbContext(POSTGRES_CONNECTION, optionsBuilder => 
                optionsBuilder.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName, 
                    SampleMigrationsDbContext.DEFAULT_SCHEMA
                )
            );
    }
}