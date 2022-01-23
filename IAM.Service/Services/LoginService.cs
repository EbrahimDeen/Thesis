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
            return Provider.GetUser(userEmail);
            //if (u1 != null && u1.Password == password)
            //{
            //    return true;
            //}
            //else return false;
        }

        public IEnumerable<User> GetUsers()
        {
            return Provider.GetUsers();
            
        }

        public void User_Register(RegisterUserRequest user)
        {
            var userList = Provider.GetUsers();
            var alradyExist=userList.Any(x => x.User_Email == user.Email);
            if (!alradyExist)
            {
                Provider.AddUser(user.Email, user.FristName, user.LastName, user.Password, user.DOB
                , user.Country, "A");

            }
            else
            {
                throw new ArgumentException("User Alrady Exist");
            }
        }
    }
}
