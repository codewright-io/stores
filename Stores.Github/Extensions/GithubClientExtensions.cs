using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public static class GithubClientExtensions
    {
        ///// <summary>
        ///// Extract a branch name from a path in this format:
        ///// git-branch:/test/en.json
        ///// </summary>
        //private static string ExtractBranchName(this string path)
        //{
        //    var splits = path.Split(':');
        //    return (splits.Length > 0) ? splits[0] : "";
        //}

        //private static string StripBranchName(this string path, string branchName)
        //{
        //    return string.IsNullOrEmpty(branchName) ? path : path.Replace($"{branchName}:", "");
        //}

        public static async Task<RepositoryContent?> GetFileContentAsync(this GitHubClient client, long repositoryId, string path, string branch)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            IReadOnlyList<RepositoryContent> content;
            try
            {
                if (string.IsNullOrEmpty(branch))
                    content = await client.Repository.Content.GetAllContents(repositoryId, path);
                else
                    content = await client.Repository.Content.GetAllContentsByRef(repositoryId, path, branch);
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
            return content.FirstOrDefault();
        }

        public static async Task<string?> GetStringContentAsync(this GitHubClient client, long repositoryId, string path, string branch)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var content = await client.GetFileContentAsync(repositoryId, path, branch);
            return content?.Content;
        }

        public static async Task UpdateStringContentAsync(this GitHubClient client, long repositoryId, string path, string content, string oldSha, string branch)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var request = string.IsNullOrEmpty(branch) ?
                  new UpdateFileRequest("Updated by gitscribe", content, oldSha)
                : new UpdateFileRequest("Updated by gitscribe", content, oldSha, branch);

            await client.Repository.Content.UpdateFile(repositoryId, path, request);
        }

        public static async Task CreateStringContentAsync(this GitHubClient client, long repositoryId, string path, string content, string branch)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var request = string.IsNullOrEmpty(branch) ?
                  new CreateFileRequest("Created by gitscribe", content)
                : new CreateFileRequest("Created by gitscribe", content, branch);

            await client.Repository.Content.CreateFile(repositoryId, path, request);
        }

        public static async Task DeleteFileAsync(this GitHubClient client, long repositoryId, string path, string oldSha, string branch)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var request = string.IsNullOrEmpty(branch) ?
                  new DeleteFileRequest("Created by gitscribe", oldSha)
                : new DeleteFileRequest("Created by gitscribe", oldSha, branch);

            await client.Repository.Content.DeleteFile(repositoryId, path, request);
        }
    }
}