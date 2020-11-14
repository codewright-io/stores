using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevKnack.Stores.Github
{
    public class SearchRepositoryQuery : ISearchRepositoryQuery
    {
        private readonly GitHubClient _client;
        private readonly RepositoryIdCache _idLookup;
        private readonly ILogger _log;

        public SearchRepositoryQuery(GitHubClient client, RepositoryIdCache idLookup, ILoggerFactory logFactory)
        {
            _client = client;
            _idLookup = idLookup;
            _log = logFactory.CreateLogger<SearchRepositoryQuery>();
        }

        public async Task<IEnumerable<string>> SearchAsync(string url, string query)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException(nameof(query));

            string branchName = url.ExtractBranchName();
            url = url.StripBranchName(branchName);

            var values = GitHubUrlExtractor.ExtractFromUrl(url);

            string username = values.Item1;
            string reponame = values.Item2;

            //var req = new SearchCodeRequest(query, username, reponame);

            //if (string.IsNullOrEmpty(branchName))
            //{
            //    var results = await _client.Search.SearchCode(req);
            //    // Github is searching the contents of the files too
            //    // Github seems generous with the match, so do a secondary filter
            //    var filteredResults = results.Items.Select(i => i.Path);

            //    return filteredResults;
            //}
            //else
            {
                if (string.IsNullOrEmpty(branchName))
                    branchName = "master";

                long repositoryId = await _idLookup.GetIdAsync(url);

                // Search in a specific branch
                TreeResponse? tree;
                try
                {
                    tree = await _client.Git.Tree.GetRecursive(repositoryId, branchName);
                }
                catch (Octokit.NotFoundException)
                {
                    _log.LogWarning($"Repository not found : {url}");
                    return Enumerable.Empty<string>();
                }

                var filePaths = tree.Tree.Select(item => item.Path);

                if (!string.IsNullOrEmpty(query))
                {
                    if (query.StartsWith("extension:"))
                    {
                        string extension = query.Replace("extension:", "");
                        filePaths = filePaths.Where(path => Path.GetExtension(path) == extension);
                    }
                    else
                    {
                        var splits = query.Split('+');
                        filePaths = filePaths.Where(path => path.Contains(splits[0]));
                    }
                }

                return filePaths;
            }
        }
    }
}