using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.ResponseModel
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; } = true;

        public int StatusCode { get; set; } = 200;

    }
}
