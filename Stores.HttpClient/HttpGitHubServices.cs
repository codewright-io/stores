using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DevKnack.Stores.HttpClient
{
    /// <summary>
    /// Client for GitHub specific services
    /// </summary>
    public class HttpGitHubServices : IGitHubServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _log;

        public HttpGitHubServices(IHttpClientFactory clientFactory, ILoggerFactory logFactory)
        {
            _clientFactory = clientFactory;
            _log = logFactory.CreateLogger<HttpGitHubServices>();
        }

        public async Task<string> GetInstallUrlAsync(string url)
        {
            var client = _clientFactory.CreateClient("default");

            string encodedUrl = StoreUrlEncoder.Encode(url);

            return await client.GetStringAsync($"api/github/v1/install-url?url={encodedUrl}");
        }

        public async Task<IEnumerable<string>> GetInstalledAppsAsync()
        {
            var client = _clientFactory.CreateClient("default");
            return await client.GetFromJsonAsync<IEnumerable<string>>("api/github/v1/installations");
        }
    }
}