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
    [Route("api/branch/v1")]
    public class BranchController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBranchService _service;

        public BranchController(IBranchService service, ILogger<BranchController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetNamesAsync(string url)
        {
            string decodedUrl = StoreUrlEncoder.Decode(url);
            return await _service.GetBranchNamesAsync(decodedUrl);
        }

        [HttpPost("create")]
        public async Task CreateAsync(BranchCreateCommand command)
        {
            string decodedUrl = StoreUrlEncoder.Decode(command.Url);

            await _service.CreateBranchAsync(decodedUrl, command.Name, command.SourceName);
        }

        [HttpPost("pull-request")]
        public async Task CreatePullRequestAsync(CreatePullRequestCommand command)
        {
            string decodedUrl = StoreUrlEncoder.Decode(command.Url);

            await _service.CreatePullRequestAsync(decodedUrl, command.Name, command.TargetName);
        }
    }
}