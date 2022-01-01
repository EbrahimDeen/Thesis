using IAM.Data.Models;
using System.Collections.Generic;

namespace IAM.API.Services
{
    public interface ILoginService
    {
        IEnumerable<User> GetUsers();
        bool AuthenticateUser(string userEmail, string password);
        void User_Register(User user);
    }
}
