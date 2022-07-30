using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Data.RequestModels
{
    public class RegisterUserRequest
    {
        [Required] public string FirstName { get; set; } = String.Empty;
        [Required] public string LastName { get; set; } = String.Empty;
        [Required] public string Email { get; set; } = String.Empty;
        [Required] public string DOB { get; set; } 
        [Required] public string Country { get; set; } = String.Empty;
        [Required] public string Password { get; set; } = String.Empty;
    }
}
