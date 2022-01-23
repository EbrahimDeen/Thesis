using IAM.API.Services;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace IAM.API.Handlers
{
    public class LoginHandler: BaseHandler
    {
        readonly ILoginService Service;
        readonly IConfiguration Configuration;
        public LoginHandler(ILoginService service, IConfiguration configuration)
        {
            Service = service;
            Configuration = configuration;
        }

        public IEnumerable<GetUserResponse> GetUsers()
        {

            var res = Service.GetUsers();
            List<GetUserResponse> getUserResponses = new List<GetUserResponse>();
            foreach (var user in res)
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
            return getUserResponses;

        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
              Configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public object Login(string userEmail , string password)
        {
            User user = null;
            string tokenString = "";
            var exp = ExecuteTryCatch(() =>
            {
                //throw new Exception("Test Exption");
                user = Service.AuthenticateUser(userEmail, password);
                if (user != null)
                {
                    tokenString = GenerateJSONWebToken(user);
                }
            });

            if (exp is null)
            {
                return string.IsNullOrWhiteSpace(tokenString) ? 
                    new UnauthorizedAccessException("Invalid Username or Password!") : tokenString;
            }
            else return exp;
        }

        public object User_Register(RegisterUserRequest user)
        {
            var exp = ExecuteTryCatch(() =>
            {
                Service.User_Register(user);
            });
            if (exp is null) return null;
            else return exp;

        }
    }
}
