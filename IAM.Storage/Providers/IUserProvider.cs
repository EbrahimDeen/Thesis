using IAM.Data.Models;
using System.Collections.Generic;

namespace IAM.Storage.Providers
{
    public interface IUserProvider
    {
        User GetUser(int id);
        User GetUser(string username);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
    }
}
