using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Domain.Aggregates;
using Sample.Domain.Aggregates.Parent;
using Sample.Services.Queries;
using Sample.Services.Queries.Models;

namespace Sample.Infrastructure.Services.Domain.Queries 
{
    public class DomainQueries : IQueries
    {

        private readonly ISampleUnitOfWorkFactory _uowFactory;

        public DomainQueries(ISampleUnitOfWorkFactory uowFactory)
        {
            _uowFactory = uowFactory;
        }

        private static SomethingDto ToSomethingDto(Parent parent) => new SomethingDto()
        {
            SomeId = parent.Id,
            SomeData = parent.Description
        };

        private async Task<TResult> InRollbackTransactionAsync<TResult>(Func<ISampleUnitOfWork, Task<TResult>> asyncOperation)
        {
            using (var uow = _uowFactory.Create())
            {
                var transactionId = await uow.BeginAsync();
                return await asyncOperation(uow);
            }
        }

        public async Task<IEnumerable<SomethingDto>> GetSomethingsAsync() => await InRollbackTransactionAsync(async uow => 
        {
            var parents = await uow.Parents.GetAsync();
            return parents.Select(ToSomethingDto);
        });

        public async Task<SomethingDto> GetSomethingByIdAsync(int id) => await InRollbackTransactionAsync(async uow => 
        {
            var parent = await uow.Parents.GetAsync(id);
            return parent != null ? ToSomethingDto(parent) : null;
        });

    }
}