using IAM.API.Handlers;
using IAM.API.Services;
using IAM.Authenticator;
using IAM.Data.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IAM.API.Controllers
{
    [ApiController]
    public class FileController : BaseController
    {
        readonly FileHandler Handler;
        public FileController(IConfiguration configuration, IFileService service) : base(configuration)
        {
            Handler = new FileHandler(service,configuration);
        }
        [HttpGet]
        [Route("TestRedis")]
        public IActionResult TestRedis()
        {
            return Ok(Redis.Ping());
        }

        [HttpPost]
        [Route("SaveFile")]
        public IActionResult SaveFile([FromBody] RequestSaveFile file)
        {
            var res = Handler.SaveFile(file);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }
    }
}
