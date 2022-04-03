using FreeRedis;
using IAM.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Authenticator
{
    public class Redis
    {
        static readonly string _redisIP = ConfigurationManager.AppSettings[Constants.RedisConfigurationIP];
        static readonly RedisClient cli = new (_redisIP);
        
        public static string Ping()
        {
            var isping = cli.Ping();
            Console.WriteLine(isping);
            return isping;
        }

        public static void Add(string key, string value)
        {
            cli.Set(key, value);
        }
        public static string Get(string key)
        {
            return cli.Get(key);    
        }
        public static void Remove(string key)
        {
            cli.Set(key, "");
        }
    }
}
