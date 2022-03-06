using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.Models
{
    public class FileMetaData
    {
        public string FileName { get; set; }
        public string Ext { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public long FileSize { get; set; }
    }
}
