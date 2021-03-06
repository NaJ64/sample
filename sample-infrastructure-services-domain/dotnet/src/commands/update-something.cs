using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Domain.Aggregates;
using Sample.Services.Commands;

namespace Sample.Infrastructure.Services.Domain.Commands
{
    public class DomainUpdateSomethingHandler : DomainHandlerBase<UpdateSomethingCommand, int>, IUpdateSomethingHandler
    {
        public DomainUpdateSomethingHandler(ISampleUnitOfWorkFactory uowFactory) : base(uowFactory) { }
        
        public override string CommandType => UpdateSomethingCommand.TYPE;

        public override async Task<int> OnHandleAsync(ISampleUnitOfWork uow, UpdateSomethingCommand command) 
        {
            var parent = await uow.Parents.GetAsync(command.SomeId);
            if (parent == null)
                throw new KeyNotFoundException($"Could not locate record for id ({command.SomeId})");
            parent.AddChild(command.SomeNewData);
            parent.Description = command.SomeNewData;
            var updated = await uow.Parents.UpdateAsync(command.SomeId, parent);
            return updated.Id;
        }
    }
}
