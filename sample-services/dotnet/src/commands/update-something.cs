using Sample.Services.Abstractions;

namespace Sample.Services.Commands
{
    public class UpdateSomethingCommand : CommandBase, ICommand
    {
        public const string TYPE = nameof(UpdateSomethingCommand);
        public override string Type => TYPE;
        public int SomeId { get; set; }
        public string SomeNewData { get; set; }
        public UpdateSomethingCommand()
        {
            SomeId = 0;
            SomeNewData = "default";
        }
    }
    
    public interface IUpdateSomethingHandler : ICommandHandler<UpdateSomethingCommand, int> { }
}