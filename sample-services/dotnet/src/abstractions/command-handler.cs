using System.Threading.Tasks;

namespace Sample.Services.Abstractions
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {
        string CommandType { get; }
        Task<TResult> HandleAsync(TCommand command);
    }

    public abstract class CommandHandlerBase<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand 
    {
        public abstract string CommandType { get; }
        public abstract Task<TResult> HandleAsync(TCommand command);
    }
}
