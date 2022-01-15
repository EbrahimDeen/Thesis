using IAM.Data.Models;
using System;
using System.Collections.Generic;

namespace IAM.Storage.Providers
{
    public interface IUserProvider
    {
        User GetUser(int id);
        User GetUser(string username);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void AddUser(string userEmail, string firstName, string lastName,
            string password, DateTime? dob, string country, string status);
    }
}
