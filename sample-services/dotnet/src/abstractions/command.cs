namespace Sample.Services.Abstractions
{
    public interface ICommand 
    {
        string Type { get; }
    }

    public abstract class CommandBase : ICommand 
    {
        public abstract string Type { get; }
    }
}
