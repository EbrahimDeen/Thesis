using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Authenticator
{
    public class Redis
    {
        static readonly RedisClient cli = new ("127.0.0.1:6379");
        
        public static string Ping()
        {
            var isping = cli.Ping();
            Console.WriteLine(isping);
            return isping;
        }

        public static void Add(string key, string value)
        {
            try
            {
                cli.Set(key, value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string Get(string key)
        {
            try
            {
                return cli.Get(key);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Remove(string key)
        {
            try
            {
                cli.Set(key, "");
            }
            catch (Exception) { throw; }
        }
    }
}
