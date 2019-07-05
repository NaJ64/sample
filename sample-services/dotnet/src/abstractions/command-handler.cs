using System.Threading.Tasks;

namespace Sample.Services.Abstractions
{

    public interface ICommandHandler
    {
        string CommandType { get; }
    }
    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> : ICommandHandler where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }

    public abstract class CommandHandlerBase 
    {
        public abstract string CommandType { get; }
    }

    public abstract class CommandHandlerBase<TCommand> : CommandHandlerBase, ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract Task HandleAsync(TCommand command);
    }

    public abstract class CommandHandlerBase<TCommand, TResult> : CommandHandlerBase, ICommandHandler<TCommand, TResult> where TCommand : ICommand 
    {
        public abstract Task<TResult> HandleAsync(TCommand command);
    }
}
