using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevKnack.Stores.Github
{
    public class RepositoryService : IRepositoryService
    {
        private readonly GitHubClient _client;

        public RepositoryService(GitHubClient client)
        {
            _client = client;
        }

        public async Task CreateRepositoryAsync(string name)
        {
            await _client.Repository.Create(new NewRepository(name));
        }

        public async Task<IEnumerable<string>> GetRepositoriesAsync()
        {
            var repositries = await _client.Repository.GetAllForCurrent();

            return repositries.Select(r => r.GitUrl).ToList();
        }
    }
}