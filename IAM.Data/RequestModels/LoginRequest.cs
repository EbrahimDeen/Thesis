using System.ComponentModel.DataAnnotations;

namespace IAM.Data.RequestModels
{
    public class LoginRequest
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
