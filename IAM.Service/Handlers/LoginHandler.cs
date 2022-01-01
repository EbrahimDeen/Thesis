using IAM.API.Services;
using IAM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IAM.API.Handlers
{
    public class LoginHandler
    {
        ILoginService Service;
        public LoginHandler(ILoginService service)
        {
            Service = service;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = Service.GetUsers();
            return users;
        }

        public bool Login(string userEmail)
        {
            var isUser = Service.GetUsers().Any(x => x.User_Email == userEmail);
            return isUser;
        }

        public void User_Register(User user)
        {
            Service.User_Register(user);
            
        }
    }
}
