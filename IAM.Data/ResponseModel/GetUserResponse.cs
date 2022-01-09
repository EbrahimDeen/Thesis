using System;

namespace IAM.Data.ResponseModel
{

    public class GetUserResponse
    {
        public int User_ID { get; set; }
        public string User_fristName { get; set; }
        public string User_lastName { get; set; }
        public string User_Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
    }
}
