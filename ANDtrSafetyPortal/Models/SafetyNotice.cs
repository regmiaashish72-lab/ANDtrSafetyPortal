using System.ComponentModel.DataAnnotations;

namespace ANDtrSafetyPortal.Models
{
    public class SafetyNotice
    {
        [Key]
        public string RecallNumber { get; set; } = "";
        public string ProductDescription { get; set; } = "";
        public string RecallClass { get; set; } = "";
        public string FirmName { get; set; } = "";
        public string RecallReason { get; set; } = "";
    }
}
