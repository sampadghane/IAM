using System.ComponentModel.DataAnnotations;
namespace AccessManagement.Models
{
    public class UserEmail
    {
        [Key]
        public string username { get; set; }
        public string email { get; set; }
        public string otp { get; set; }
        public string validity { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public UserEmail()
        {
            otp = "-1";
        }

    }
}