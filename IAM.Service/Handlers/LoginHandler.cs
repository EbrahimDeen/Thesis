using IAM.API.Services;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IAM.API.Handlers
{
    public class LoginHandler: BaseHandler
    {
        ILoginService Service;
        public LoginHandler(ILoginService service)
        {
            Service = service;
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
        public object Login(string userEmail , string password)
        {
            //throw new Exception("Test Exption");
            //var isUser = Service.AuthenticateUser(userEmail, password);
            //return isUser;

            bool isUser = false;
            var exp = ExecuteTryCatch(() =>
            {
                //throw new Exception("Test Exption");
                isUser = Service.AuthenticateUser(userEmail, password);
            });

            if (exp is null) return isUser;
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
