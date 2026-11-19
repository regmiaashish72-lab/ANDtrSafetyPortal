using System.ComponentModel.DataAnnotations;

namespace ANDtrSafetyPortal.Models
{
    public class RiskReview
    {
        [Key]
        public int ReviewID { get; set; }
        public int EmployeeID { get; set; }
        public string Title { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
