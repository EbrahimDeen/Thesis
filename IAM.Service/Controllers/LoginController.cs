using IAM.API.Handlers;
using IAM.API.Services;
using IAM.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Service.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        readonly LoginHandler Handler;
        public LoginController(ILoginService service)
        {
            Handler = new LoginHandler(service);
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var res = Handler.GetUsers(); 
            return Ok(res);
        }
        [HttpGet]
        [Route("Login/{username}")]
        public IActionResult Login(string username)
        {
            var res = Handler.Login(username);
            return Ok(res);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult User_Register(User user)
        {
            try
            {
                Handler.User_Register(user);
                return Ok();            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
