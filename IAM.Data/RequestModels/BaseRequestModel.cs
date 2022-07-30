using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.RequestModels
{
    public class BaseRequestModel
    {
        [Required]
        public string Token { get; set; }
    }
}
