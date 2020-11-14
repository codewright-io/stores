using DevKnack.Common.Exceptions;
using DevKnack.Stores.AspNetCore.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores.AspNetCore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/store/v1")]
    public class StoreController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFileStoreFactory _storeFactory;

        public StoreController(IFileStoreFactory storeFactory, ILogger<StoreController> logger)
        {
            _logger = logger;
            _storeFactory = storeFactory;
        }

        [HttpGet]
        public async Task<string?> GetAsync(string url, string path)
        {
            string decodedUrl = StoreUrlEncoder.Decode(url);
            string decodedPath = StoreUrlEncoder.Decode(path);

            // TODO: User context for repository ID & login
            var store = _storeFactory.Create(decodedUrl);
            if (store == null)
            {
                _logger.LogWarning("No store match for get {0}", decodedUrl);
                throw new NotFoundException("Protocol not registered", decodedUrl);
            }
            return await store.ReadStringFileAsync(decodedUrl, decodedPath);
        }

        [HttpPut]
        [HttpPost]
        public async Task PutAsync(StorePutCommand command)
        {
            string decodedUrl = StoreUrlEncoder.Decode(command.Url);
            string decodedPath = StoreUrlEncoder.Decode(command.Path);

            var store = _storeFactory.Create(decodedUrl);
            if (store == null)
            {
                _logger.LogWarning("No store match for put {0}", decodedUrl);
                throw new NotFoundException("Protocol not registered", decodedUrl);
            }
            await store.WriteStringFileAsync(decodedUrl, decodedPath, command.Contents);
        }

        [HttpDelete]
        public async Task DeleteAsync(string url, string path)
        {
            string decodedUrl = StoreUrlEncoder.Decode(url);
            string decodedPath = StoreUrlEncoder.Decode(path);

            var store = _storeFactory.Create(decodedUrl);
            if (store == null)
            {
                _logger.LogWarning("No store match for put {0}", decodedUrl);
                throw new NotFoundException("Protocol not registered", decodedUrl);
            }
            await store.DeleteFileAsync(decodedUrl, decodedPath);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<string>> GetListAsync([FromServices] IRepositoryQuery query)
        {
            return await query.GetRepositoriesAsync();
        }

        [HttpPost("create")]
        public async Task CreateAsync([FromServices] IRepositoryService service, RepositoryCreateCommand command)
        {
            await service.CreateRepositoryAsync(command.Name);
        }

        [HttpGet("search")]
        public async Task<IEnumerable<string>> SearchAsync([FromServices] ISearchRepositoryQuery searchQuery, string url, string query)
        {
            string decodedUrl = StoreUrlEncoder.Decode(url);

            return await searchQuery.SearchAsync(decodedUrl, query);
        }
    }
}