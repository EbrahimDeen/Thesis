using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Storage
{
    public class AnalysisTempDB
    {
        public class Analysis
        {
            public string ID { get; set; }
            public string IP { get; set; }
            public string CountryName { get; set; }
            public string CityName { get; set; }
            public string ContinentName { get; set; }
            public string DownloadedBy { get; set; }
            public string OwnerID { get; set; }
            public string FileID { get; set; }
        }
    }
}
