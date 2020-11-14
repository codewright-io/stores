using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface IRepositoryQuery
    {
        Task<IEnumerable<string>> GetRepositoriesAsync();
    }
}