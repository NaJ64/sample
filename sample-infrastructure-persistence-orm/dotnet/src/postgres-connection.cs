using System;

namespace Sample.Infrastructure.Persistence.ORM
{
    public class PostgresConnection
    { 
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string ToConnectionString() => 
            $"Server=${Host};Port=${Port};Database=${Database};User Id=${Username};Password=${Password}";
    }
}