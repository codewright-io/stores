using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores.AspNetCore.Controllers
{
    /// <summary>
    /// GitHub specific API calls
    /// </summary>
    [ApiController]
    [Route("api/github/v1")]
    public class GitHubController : ControllerBase
    {
        [HttpGet("install-url"), Authorize]
        public async Task<string> GetInstallUrlAsync([FromServices] IGitHubServices installService, [FromQuery] string url)
        {
            return await installService.GetInstallUrlAsync(url);
        }

        [HttpGet("installations"), Authorize]
        public async Task<IEnumerable<string>> GetInstallationsAsync([FromServices] IGitHubServices installService)
        {
            return await installService.GetInstalledAppsAsync();
        }
    }
}