using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.RequestModels
{
    public class FileAnalysisRequest
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string Continent { get; set; }
        public string IPAddress { get; set; }
    }
}
