using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAM.Data.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int User_ID { get; set; }
        public string User_fristName { get; set; }
        public string User_lastName { get; set; }
        public string User_Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }



    }
}
