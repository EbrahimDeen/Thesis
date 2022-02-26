using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.RequestModels
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string DOB { get; set; } 
        public string Country { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
