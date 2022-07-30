using IAM.API.Controllers;
using IAM.API.Handlers;
using IAM.API.Services;
using IAM.Authenticator;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace IAM.Service.Controllers
{
    [ApiController]
    public class LoginController : BaseController
    {
        readonly LoginHandler Handler;
        public LoginController(ILoginService service, IConfiguration configuration, IAuthenticator authenticator): base(configuration)
        {
            Handler = new LoginHandler(service, configuration, authenticator);
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var res = Handler.GetUsers();
            var env = GetResult(res);
            return StatusCodeResult(env);
            
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var res = Handler.Login(request.UserEmail, request.Password);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult User_Register(RegisterUserRequest request)
        {
            var res = Handler.User_Register(request);
            var env = GetResult(res);
            return StatusCodeResult(env);

        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout(BaseRequestModel request)
        {
            var res = Handler.Logout(request.Token);
            var env = GetResult(res);
            return StatusCodeResult(env);
        }
    }
}
