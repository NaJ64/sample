using Sample.Services.Abstractions;

namespace Sample.Services.Commands
{
    public class AddSomethingCommand : CommandBase, ICommand
    {
        public const string TYPE = nameof(AddSomethingCommand);
        public override string Type => TYPE;
        public string SomeNewData { get; set; }
        public AddSomethingCommand() 
        {
            SomeNewData = "default";
        }
    }

    public interface IAddSomethingCommandHandler : ICommandHandler<AddSomethingCommand, int> { }
}