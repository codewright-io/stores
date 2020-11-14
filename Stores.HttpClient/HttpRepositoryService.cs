using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DevKnack.Stores.HttpClient
{
    public class HttpRepositoryService : IRepositoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _log;

        public HttpRepositoryService(IHttpClientFactory clientFactory, ILoggerFactory logFactory)
        {
            _clientFactory = clientFactory;
            _log = logFactory.CreateLogger<HttpFileStore>();
        }

        public async Task CreateRepositoryAsync(string name)
        {
            var client = _clientFactory.CreateClient("default");

            var request = new
            {
                Name = name
            };

            await client.PostAsJsonAsync("api/store/v1/create", request);
        }

        public async Task<IEnumerable<string>> GetRepositoriesAsync()
        {
            var client = _clientFactory.CreateClient("default");

            return await client.GetFromJsonAsync<IEnumerable<string>>("api/store/v1/list");
        }
    }
}