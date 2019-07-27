using System;
using Sample.Infrastructure.Persistence.ORM;

namespace Sample.Infrastructure.DependencyInjection
{
    public interface IOptions 
    { 
        bool DomainQueries { get; set; }
        bool DomainCommands { get; set; }
        PostgresConnection Postgres { get; set; }
        Nullable<OrmType> ORM { get; set; }
    }
    
    public class Options : IOptions
    { 
        public bool DomainQueries { get; set; } = true;
        public bool DomainCommands { get; set; } = true;
        public PostgresConnection Postgres { get; set; }
        public Nullable<OrmType> ORM { get; set; }
    }
}