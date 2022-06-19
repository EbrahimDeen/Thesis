using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.RequestModels
{
    public class RequestSaveFile : BaseRequestModel
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string Ext { get; set; }
    }

    public class RequestSaveEncryptedFile : BaseRequestModel
    {
        public string HexData { get; set; }
        public string FileName { get; set; }
        public string Ext { get; set; }
    }

}
