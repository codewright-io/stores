using Microsoft.Extensions.Logging;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevKnack.Stores.Github
{
    public class BranchService : IBranchService
    {
        private readonly GitHubClient _client;
        private readonly ILogger _log;

        public BranchService(GitHubClient client, ILoggerFactory logFactory)
        {
            _client = client;
            _log = logFactory.CreateLogger<BranchService>();
        }

        public async Task<bool> CreateBranchAsync(string url, string name, string sourceName)
        {
            var values = GitHubUrlExtractor.ExtractFromUrl(url);

            string username = values.Item1;
            string reponame = values.Item2;

            var branch = await _client.Repository.Branch.Get(username, reponame, sourceName);
            if (branch == null)
            {
                _log.LogError("Source branch not found");
                return false;
            }

            var reference = await _client.Git.Reference.Create(username, reponame, new NewReference($"refs/heads/{name}", branch.Commit.Sha));
            if (reference == null)
            {
                _log.LogError("Branch not created");
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<string>> GetBranchNamesAsync(string url)
        {
            var values = GitHubUrlExtractor.ExtractFromUrl(url);

            string username = values.Item1;
            string reponame = values.Item2;

            var branches = await _client.Repository.Branch.GetAll(username, reponame);

            var names = branches.Select(b => b.Name);

            // Put master & develop at the top of the branch list
            return names.OrderByDescending(n => n == "master" || n == "develop");
        }

        public async Task CreatePullRequestAsync(string url, string name, string targetName)
        {
            var values = GitHubUrlExtractor.ExtractFromUrl(url);

            string username = values.Item1;
            string reponame = values.Item2;

            await _client.PullRequest.Create(username, reponame, new NewPullRequest("Gitscribe translation", name, targetName));
        }
    }
}