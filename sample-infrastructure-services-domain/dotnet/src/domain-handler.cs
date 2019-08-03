using System;
using System.Threading.Tasks;
using Sample.Services.Abstractions;
using Sample.Domain.Aggregates;

namespace Sample.Infrastructure.Services.Domain
{
    public abstract class DomainHandlerBase<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand
    {

        protected readonly ISampleUnitOfWorkFactory _uowFactory;

        public abstract string CommandType { get; }

        public DomainHandlerBase(ISampleUnitOfWorkFactory uowFactory) => _uowFactory = uowFactory;

        public abstract Task<TResult> OnHandleAsync(ISampleUnitOfWork uow, TCommand command);

        public async Task<TResult> HandleAsync(TCommand command)
        {
            TResult result = default(TResult);
            using (var uow = _uowFactory.Create())
            {
                var transactionId = await uow.BeginAsync();
                result = await OnHandleAsync(uow, command);
                await uow.CommitAsync();
            }
            return result;
        }

    }
}
