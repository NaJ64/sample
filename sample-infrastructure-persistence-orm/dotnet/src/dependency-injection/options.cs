using System;

namespace Sample.Infrastructure.Persistence.ORM.DependencyInjection
{
    public interface IOptions
    { 
        OrmType ORM { get; set; }
        PostgresConnection Postgres { get; set; }
    }

    public class Options : IOptions 
    { 
        public OrmType ORM { get; set; }
        public PostgresConnection Postgres { get; set; }
    }
}