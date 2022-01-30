using IAM.Authenticator;
using IAM.Data;
using System;

namespace IAM.API.Services
{
    public class RedisService : IRedis
    {
        readonly string KeyTemplate = "{0}:{1}";
        public void Add<T>(string key, string value)
        {
            var k = string.Format(KeyTemplate, typeof(T).FullName, key);
            Redis.Add(k, value);
        }
        public void AddUserSession(string key, string value)
        {
            var k = string.Format(KeyTemplate, Constants.USERKEY, key);
            Console.WriteLine(k);
            Redis.Add(k, value);
        }

        public string Get<T>(string key)
        {
            var k = string.Format(KeyTemplate, typeof(T).FullName, key);
            return Redis.Get(k);
        }
        public string GetUserSession(string key)
        {
            var k = string.Format(KeyTemplate, Constants.USERKEY, key);
            return Redis.Get(k);
        }
        public void Remove<T>(string key)
        {
            var k = string.Format(KeyTemplate, typeof(T).FullName, key);
            Redis.Remove(k);
        }
        public void Remove(string key)
        {
            var k = string.Format(KeyTemplate, Constants.USERKEY, key);
            Redis.Remove(k);
        }
    }
}
