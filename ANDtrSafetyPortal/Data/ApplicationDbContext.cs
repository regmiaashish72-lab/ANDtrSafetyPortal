
using Microsoft.EntityFrameworkCore;
using ANDtrSafetyPortal.Models;

namespace ANDtrSafetyPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SafetyNotice> SafetyNotices { get; set; }
        public DbSet<RiskReview> RiskReviews { get; set; }
        public DbSet<ReviewNotice> ReviewNotices { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SharedFinding> SharedFindings { get; set; }
    }
}
