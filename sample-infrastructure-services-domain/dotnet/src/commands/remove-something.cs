using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Domain.Aggregates;
using Sample.Services.Commands;

namespace Sample.Infrastructure.Services.Domain.Commands
{
    public class RemoveSomethingHandler : DomainHandlerBase<RemoveSomethingCommand, int>, IRemoveSomethingCommandHandler
    {
        public override string CommandType => RemoveSomethingCommand.TYPE;

        protected override async Task<int> OnHandleAsync(ISampleUnitOfWork uow, RemoveSomethingCommand command) 
        {
            var parent = await uow.Parents.GetAsync(command.SomeId);
            if (parent == null)
                throw new KeyNotFoundException($"Could not locate record for id ({command.SomeId})");
            await uow.Parents.DeleteAsync(command.SomeId);
            return 0;
        }
    }
}
