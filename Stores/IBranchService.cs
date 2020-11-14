using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface IBranchService
    {
        Task<bool> CreateBranchAsync(string url, string name, string sourceName);

        Task<IEnumerable<string>> GetBranchNamesAsync(string url);

        Task CreatePullRequestAsync(string url, string name, string targetName);
    }
}