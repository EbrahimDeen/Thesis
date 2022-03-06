using IAM.Data.Models;
using IAM.Storage.Providers;
using System;

namespace IAM.Authenticator
{
    public class Authenticate : IAuthenticator
    {
        readonly IUserProvider _userProvider;
        //string KEY = @"SESSION:";
        public Authenticate(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        public void AddUserToken(string token, string userId)
        {
            try
            {
                Redis.Add(token, userId);
            }
            catch (Exception)
            {
                //log ex
                throw;
            }
        }

        public User AuthToken(string token)
        {
            try
            {
                var tokenValue = Redis.Get(token);
                if(tokenValue != null)
                {
                    var userID = Convert.ToInt32(tokenValue);
                    var user = _userProvider.GetUser(userID);
                    return user;
                }
                throw new ArgumentException("Invalid Session Id!");
                //return null;
            }
            catch (Exception)
            {
                //log ex
                throw;
            }
        }
    }
}
