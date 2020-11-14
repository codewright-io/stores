using System.Linq;

namespace DevKnack.Stores.Github
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Extract a branch name from a path in this format:
        /// git-branch:/test/en.json
        /// </summary>
        public static string ExtractBranchName(this string url)
        {
            var splits = url.Split('#');
            return (splits.Length > 1) ? splits.Last() : "";
        }

        public static string StripBranchName(this string url, string branchName)
            => string.IsNullOrEmpty(branchName) ? url : url.Replace($"#{branchName}", "");
    }
}