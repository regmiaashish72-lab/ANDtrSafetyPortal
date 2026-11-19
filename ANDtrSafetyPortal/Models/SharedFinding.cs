using System.ComponentModel.DataAnnotations;

namespace ANDtrSafetyPortal.Models
{
    public class SharedFinding
    {
        [Key]
        public int ShareID { get; set; }
        public int ReviewID { get; set; }
        public int CustomerID { get; set; }
        public DateTime SharedAt { get; set; }
    }
}
