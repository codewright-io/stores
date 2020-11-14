using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores
{
    /// <summary>
    /// Github specific API calls not covered by the shared stores interfaces
    /// </summary>
    /// <remarks>If this functionality ends up being shared by a few git providers, then think about creating a more general interface</remarks>

    public interface IGitHubServices
    {
        Task<string> GetInstallUrlAsync(string url);

        Task<IEnumerable<string>> GetInstalledAppsAsync();
    }
}