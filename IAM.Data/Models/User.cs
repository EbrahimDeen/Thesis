using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAM.Data.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }



    }
}
