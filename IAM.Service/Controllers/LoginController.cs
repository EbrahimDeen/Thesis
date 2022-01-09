using IAM.API.Handlers;
using IAM.API.Services;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
            List <GetUserResponse> getUserResponses = new List<GetUserResponse>();
            foreach (var user in res.Data)
            {
                GetUserResponse response = new GetUserResponse()
                {
                    Country = user.Country,
                    User_Email = user.User_Email,
                    DOB = user.DOB,
                    Status = user.Status,
                    User_fristName = user.User_fristName,
                    User_ID = user.User_ID,
                    User_lastName = user.User_lastName
                };
                getUserResponses.Add(response);
            }
            
            return Ok(res);
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
 
            var res = Handler.Login(request.UserEmail, request.Password);
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
