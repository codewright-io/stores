using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DevKnack.Stores.HttpClient
{
    /// <summary>
    /// Storage over our stores API
    /// </summary>
    public class HttpBranchService : IBranchService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _log;

        public HttpBranchService(IHttpClientFactory clientFactory, ILoggerFactory logFactory)
        {
            _clientFactory = clientFactory;
            _log = logFactory.CreateLogger<HttpFileStore>();
        }

        public async Task<bool> CreateBranchAsync(string url, string name, string sourceName)
        {
            var client = _clientFactory.CreateClient("default");

            string encodedUrl = StoreUrlEncoder.Encode(url);

            var request = new
            {
                Url = encodedUrl,
                Name = name,
                SourceName = sourceName
            };

            var response = await client.PostAsJsonAsync("api/branch/v1/create", request);

            return response.IsSuccessStatusCode;
        }

        public async Task CreatePullRequestAsync(string url, string name, string targetName)
        {
            var client = _clientFactory.CreateClient("default");

            string encodedUrl = StoreUrlEncoder.Encode(url);

            var request = new
            {
                Url = encodedUrl,
                Name = name,
                TargetName = targetName
            };

            await client.PostAsJsonAsync("api/branch/v1/pull-request", request);
        }

        public async Task<IEnumerable<string>> GetBranchNamesAsync(string url)
        {
            var client = _clientFactory.CreateClient("default");

            string encodedUrl = StoreUrlEncoder.Encode(url);

            return await client.GetFromJsonAsync<IEnumerable<string>>($"api/branch/v1?url={encodedUrl}");
        }
    }
}