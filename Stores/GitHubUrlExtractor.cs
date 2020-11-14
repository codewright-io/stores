using System;

namespace DevKnack.Stores
{
    public static class GitHubUrlExtractor
    {
        public static (string, string) ExtractFromUrl(string url)
        {
            // TODO: We should be a bit smarter about the replace and only remove at beginning & end

            // e.g. https://github.com/User/GitScribe
            var splits = url.Replace("github.com/", "")
                            .Replace("https://", "")
                            .Replace("git://", "")
                            .Replace(".git", "")
                            .Split('/');

            if (splits.Length < 2)
                throw new ArgumentException("Github URL in bad format", nameof(url));

            string username = splits[0];
            string reponame = splits[1];

            return (username, reponame);
        }
    }
}