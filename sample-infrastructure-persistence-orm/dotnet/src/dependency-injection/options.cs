using System;

namespace Sample.Infrastructure.Persistence.ORM.DependencyInjection
{
    public struct PostgresConnection 
    { 
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }

    public interface IOptions
    { 
        Nullable<PostgresConnection> Postgres { get; set; }
    }

    public class Options : IOptions 
    { 
        public Nullable<PostgresConnection> Postgres { get; set; }
    }
}