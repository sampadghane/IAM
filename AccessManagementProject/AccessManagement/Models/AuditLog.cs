using System.ComponentModel.DataAnnotations;

namespace AccessManagement.Models
{
    public class AuditLog
    {
        public string User { get; set; }

        public string action { get; set; }

        [Key]
        public string TimeStamp { get; set; }


    }
}
