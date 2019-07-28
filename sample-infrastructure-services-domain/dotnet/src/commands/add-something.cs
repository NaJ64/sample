using System.Threading.Tasks;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Services.Commands;

namespace Sample.Infrastructure.Services.Domain.Commands
{
    public class DomainAddSomethingHandler : DomainHandlerBase<AddSomethingCommand, int>, IAddSomethingHandler
    {
        public DomainAddSomethingHandler(ISampleUnitOfWorkFactory uowFactory) : base(uowFactory) { }
        
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
