
namespace ANDtrSafetyPortal.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ReviewNoticeID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; } = "";
    }
}
