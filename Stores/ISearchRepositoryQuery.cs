using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface ISearchRepositoryQuery
    {
        Task<IEnumerable<string>> SearchAsync(string url, string query);
    }
}