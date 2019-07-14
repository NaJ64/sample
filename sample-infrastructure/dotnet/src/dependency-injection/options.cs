using System;
using Sample.Infrastructure.Persistence.ORM.DependencyInjection;

namespace Sample.Infrastructure.DependencyInjection
{
    public interface IOptions 
    { 
        bool DomainQueries { get; set; }
        bool DomainCommands { get; set; }
        Nullable<PostgresConnection> Postgres { get; set; }
    }
    
    public class Options : IOptions
    { 
        public bool DomainQueries { get; set; } = true;
        public bool DomainCommands { get; set; } = true;
        public Nullable<PostgresConnection> Postgres { get; set; }
    }
}