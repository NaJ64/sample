using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Services.Queries.Models;

namespace Sample.Services.Queries
{
    public interface IQueries
    {
        Task<IEnumerable<SomethingDto>> GetSomethingsAsync();
        Task<SomethingDto> GetSomethingByIdAsync(int id);
    }
}