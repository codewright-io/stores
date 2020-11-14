using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface IGitStore : IFileStore
    {
        Task CreatePullRequestAsync();
    }
}
