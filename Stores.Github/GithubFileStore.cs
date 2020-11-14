using Octokit;
using System.Threading.Tasks;

namespace DevKnack.Stores.Github
{
    public class GithubFileStore : IFileStore
    {
        private readonly GitHubClient _client;
        private readonly RepositoryIdCache _idLookup;

        // TODO: Add current branch requester!

        public GithubFileStore(GitHubClient client, RepositoryIdCache idLookup)
        {
            _client = client;
            _idLookup = idLookup;
        }

        //public async Task<byte[]> ReadBinaryFileAsync(string url, string path)
        //{
        //    long repositoryId = await _idLookup.GetIdAsync(url);

        //    var content = await _client.GetFileContentAsync(repositoryId, path);
        //    return Convert.FromBase64String(content.EncodedContent);
        //}

        public async Task<string?> ReadStringFileAsync(string url, string path)
        {
            string branchName = url.ExtractBranchName();
            url = url.StripBranchName(branchName);

            long repositoryId = await _idLookup.GetIdAsync(url);

            string? content = await _client.GetStringContentAsync(repositoryId, path, branchName);
            return content;
        }

        public async Task WriteStringFileAsync(string url, string path, string contents)
        {
            string branchName = url.ExtractBranchName();
            url = url.StripBranchName(branchName);

            long repositoryId = await _idLookup.GetIdAsync(url);

            var fileContent = await _client.GetFileContentAsync(repositoryId, path, branchName);
            if (fileContent != null)
            {
                await _client.UpdateStringContentAsync(repositoryId, path, contents, fileContent.Sha, branchName);
            }
            else
            {
                await _client.CreateStringContentAsync(repositoryId, path, contents, branchName);
            }
        }

        public async Task DeleteFileAsync(string url, string path)
        {
            string branchName = url.ExtractBranchName();
            url = url.StripBranchName(branchName);

            long repositoryId = await _idLookup.GetIdAsync(url);

            var fileContent = await _client.GetFileContentAsync(repositoryId, path, branchName);
            if (fileContent != null)
            {
                await _client.DeleteFileAsync(repositoryId, path, fileContent.Sha, branchName);
            }
        }
    }
}