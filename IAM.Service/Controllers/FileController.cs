using IAM.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IAM.API.Controllers
{
    [ApiController]
    public class FileController : BaseController
    {
        public FileController(IConfiguration configuration) : base(configuration)
        {
        }
        [HttpGet]
        [Route("TestRedis")]
        public IActionResult TestRedis()
        {
            Redis.TestResult();
            return Ok();
        }
    }
}
