using IAM.API.Services;
using IAM.Data;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IAM.API.Handlers
{
    public class LoginHandler: BaseHandler
    {
        readonly ILoginService Service;
        readonly IConfiguration Configuration;
        IRedis Redis;
        public LoginHandler(ILoginService service, IConfiguration configuration, IRedis redis)
        {
            Service = service;
            Configuration = configuration;
            Redis = redis;
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
                    User_Email = user.Email,
                    DOB = user.DOB,
                    Status = user.Status,
                    User_fristName = user.FristName,
                    User_ID = user.ID,
                    User_lastName = user.LastName
                };
                getUserResponses.Add(response);
            }
            return getUserResponses;

        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.FristName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("UserID", userInfo.ID.ToString()),
            };


            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
              Configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public object Login(string userEmail , string password)
        {
            string tokenString = "";
            var exp = ExecuteTryCatch(() =>
            {
                var user = Service.AuthenticateUser(userEmail, password);
                if (user != null)
                {
                    tokenString = GenerateJSONWebToken(user);
                    Redis.AddUserSession(user.ID.ToString(), tokenString);
                }
            });

            if (exp is null)
            {
                return string.IsNullOrWhiteSpace(tokenString) ? 
                    new UnauthorizedAccessException("Invalid Username or Password!") : 
                    new LoginResponseModel() { Token = tokenString};
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
