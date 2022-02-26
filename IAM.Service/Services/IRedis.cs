namespace IAM.API.Services
{
    public interface IRedis
    {
        void Add<T>(string key, string value);
        void AddUserSession(string key, string value);
        void Remove<T>(string key);
        void Remove(string key);
        string Get<T>(string key);
        string GetUserSession(string key);
    }
}
