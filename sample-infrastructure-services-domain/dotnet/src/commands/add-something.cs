using System.Threading.Tasks;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Services.Commands;

namespace Sample.Infrastructure.Services.Domain.Commands
{
    public class AddSomethingHandler : DomainHandlerBase<AddSomethingCommand, int>, IAddSomethingCommandHandler
    {
        public override string CommandType => AddSomethingCommand.TYPE;

        protected override async Task<int> OnHandleAsync(ISampleUnitOfWork uow, AddSomethingCommand command) 
        {
            var newRecord = new Parent(command.SomeNewData);
            newRecord.AddChild(newRecord.Description);
            var inserted = await uow.Parents.InsertAsync(newRecord);
            return inserted.Id;
        }
    }
}
