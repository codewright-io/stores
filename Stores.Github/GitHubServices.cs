using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevKnack.Stores.Github
{
    /// <summary>
    /// Github specific API calls not covered by the shared stores interfaces
    /// </summary>
    /// <remarks>If this functionality ends up being shared by a few git providers, then think about creating a common interface</remarks>
    public class GitHubServices : IGitHubServices
    {
        private readonly GitHubClient _client;
        private readonly GithubAuthSettings _appSettings;
        private readonly RepositoryIdCache _idLookup;

        public GitHubServices(GitHubClient client, GithubAuthSettings appSettings, RepositoryIdCache idLookup)
        {
            _client = client;
            _appSettings = appSettings;
            _idLookup = idLookup;
        }

        public async Task<string> GetInstallUrlAsync(string url)
        {
            string appName = "GitScribe-Develop";
            // https://github.com/apps/YOUR_APP_NAME/installations/new/permissions?suggested_target_id=ID_OF_USER_OR_ORG&repository_ids[]=REPO_A_ID&repository_ids[]=REPO_B_ID

            var currentUser = await _client.User.Current();
            int userId = currentUser.Id;
            long repoId = await _idLookup.GetIdAsync(url);

            string installUrl = $"https://github.com/apps/{appName}/installations/new/permissions?suggested_target_id={userId}&repository_ids[]={repoId}";

            //foreach (string repoId in repositoryIds)
            //{
            //    url += $"&repository_ids[]={repoId}";
            //}

            return installUrl;
        }

        public async Task<IEnumerable<string>> GetInstalledAppsAsync()
        {
            // This returns : Resource not accessible by integration
            // var repos = await _client.GitHubApps.Installation.GetAllRepositoriesForCurrent();

            var response = await _client.GitHubApps.GetAllInstallationsForCurrentUser();

            long installationId = response.Installations.First(i => i.AppId == _appSettings.AppId).Id;
            //long installationId = _appSettings.AppId;

            var repos = await _client.GitHubApps.Installation.GetAllRepositoriesForCurrentUser(installationId);

            return repos.Repositories.Select(r => r.GitUrl);
        }
    }
}