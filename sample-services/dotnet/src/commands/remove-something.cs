using Sample.Services.Abstractions;

namespace Sample.Services.Commands
{
    public class RemoveSomethingCommand : CommandBase, ICommand
    {
        public const string TYPE = nameof(RemoveSomethingCommand);
        public override string Type => TYPE;
        public int SomeId { get; set; }
        public RemoveSomethingCommand() 
        {
            SomeId = 0;
        }
    }

    public interface IRemoveSomethingCommandHandler : ICommandHandler<RemoveSomethingCommand, int> { }
}