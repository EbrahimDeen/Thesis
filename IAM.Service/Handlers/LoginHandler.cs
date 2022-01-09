using IAM.API.Services;
using IAM.Data.Models;
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

        public Result<IEnumerable<User>> GetUsers()
        {
            return HandleResult(() => Service.GetUsers());

        }
        public Result<bool> Login(string userEmail , string password)
        {
            return HandleResult(() =>
            {
                var isUser = Service.AuthenticateUser(userEmail, password);
                return isUser;
            });
        }

        public void User_Register(User user)
        {
            try
            {
                Service.User_Register(user);
            }
            catch (Exception)
            {

                throw;
            } 
            
        }
    }
}
