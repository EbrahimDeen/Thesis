using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Storage.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IAM.API.Services
{
    public class LoginService : ILoginService
    {
        readonly IUserProvider Provider;
        public LoginService(IUserProvider provider)
        {
            Provider = provider;
        }
        public User AuthenticateUser(string userEmail, string password)
        {
            var user = Provider.GetUser(userEmail);
            if(user != null)
            {
                if(user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            return Provider.GetUsers();
            
        }

        public void User_Register(RegisterUserRequest user)
        {
            var userList = Provider.GetUsers();
            var alradyExist=userList.Any(x => x.Email == user.Email);
            if (!alradyExist)
            {
                Provider.AddUser(user.Email, user.FirstName, user.LastName, user.Password, DateTime.Parse(user.DOB)
                , user.Country, "A");

            }
            else
            {
                throw new ArgumentException("User Alrady Exist");
            }
        }
    }
}
