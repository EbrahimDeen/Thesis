using IAM.Data.Models;

namespace IAM.Authenticator
{
    public interface IAuthenticator
    {
        void AddUserToken(string token, string userId);
        User AuthToken(string token);
        void RemoveUserToken(string token);
    }
}