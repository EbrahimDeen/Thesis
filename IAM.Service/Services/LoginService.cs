using IAM.Data.Models;
using IAM.Storage.Providers;
using System;
using System.Collections.Generic;

namespace IAM.API.Services
{
    public class LoginService : ILoginService
    {
        readonly IUserProvider Provider;
        public LoginService(IUserProvider provider)
        {
            Provider = provider;
        }
        public bool AuthenticateUser(string userEmail, string password)
        {
            var u1 = Provider.GetUser(userEmail);
            if (u1 != null && u1.Password == password)
            {
                return true;
            }
            else return false;
        }

        public IEnumerable<User> GetUsers()
        {
            return Provider.GetUsers();
            
        }

        public void User_Register(User user)
        {
            Provider.AddUser(user);
        }
    }
}
