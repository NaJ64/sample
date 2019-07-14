namespace Sample.Infrastructure.Services.Domain.DependencyInjection
{
    public interface IOptions 
    { 
        bool EnableQueries { get; set; }
        bool EnableCommands { get; set; }
    }
    
    public class Options : IOptions 
    { 
        public bool EnableQueries { get; set; } = true;
        public bool EnableCommands { get; set; } = true;
    }
}