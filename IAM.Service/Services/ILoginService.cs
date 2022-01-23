using IAM.Data.Models;
using IAM.Data.RequestModels;
using System.Collections.Generic;

namespace IAM.API.Services
{
    public interface ILoginService
    {
        IEnumerable<User> GetUsers();
        User AuthenticateUser(string userEmail, string password);
        void User_Register(RegisterUserRequest user);
    }
}
