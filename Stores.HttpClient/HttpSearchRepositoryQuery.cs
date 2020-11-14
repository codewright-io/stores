using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace DevKnack.Stores.HttpClient
{
    public class HttpSearchRepositoryQuery : ISearchRepositoryQuery
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _log;

        public HttpSearchRepositoryQuery(IHttpClientFactory clientFactory, ILoggerFactory logFactory)
        {
            _clientFactory = clientFactory;
            _log = logFactory.CreateLogger<HttpFileStore>();
        }

        public async Task<IEnumerable<string>> SearchAsync(string url, string query)
        {
            var client = _clientFactory.CreateClient("default");

            string encodedUrl = StoreUrlEncoder.Encode(url);
            string encodedQuery = HttpUtility.UrlEncode(query);

            return await client.GetFromJsonAsync<IEnumerable<string>>($"api/store/v1/search?url={encodedUrl}&query={encodedQuery}");
        }
    }
}