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
        static RedisClient cli = new ("127.0.0.1:6379");
        
        public static void TestResult()
        {
            var isping = cli.Ping();
            Console.WriteLine(isping);
            //cli.Notice += (s, e) => Console.WriteLine(e.Log);
            //cli.Set("key1", "value1");
            //cli.MSet("key1", "value1", "key2", "value2");
            //string value1 = cli.Get("key1");
            //string[] vals = cli.MGet("key1", "key2");

        }
    }
}
