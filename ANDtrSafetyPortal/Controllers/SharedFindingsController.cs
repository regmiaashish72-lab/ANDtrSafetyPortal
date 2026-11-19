
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ANDtrSafetyPortal.Data;

namespace ANDtrSafetyPortal.Controllers
{
    public class SharedFindingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SharedFindingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Temporary test customer (ID 2)
            // Later this will come from the logged-in account.
            int customerId = 2;

            var findings = await _context.SharedFindings
                .Where(f => f.CustomerID == customerId)
                .ToListAsync();

            return View(findings);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Temporary customer ID for testing
            int customerId = 2;

            // Check that this review was shared with this customer
            var sharedFinding = await _context.SharedFindings
                .FirstOrDefaultAsync(f =>
                    f.ReviewID == id &&
                    f.CustomerID == customerId);

            if (sharedFinding == null)
            {
                return NotFound();
            }

            // Retrieve the actual risk review
            var review = await _context.RiskReviews
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (review == null)
            {
                return NotFound();
            }

            var linkedNotices = await _context.ReviewNotices
                .Where(n => n.ReviewID == id)
                .Select(n => n.RecallNumber)
                .ToListAsync();

            ViewBag.LinkedNotices = linkedNotices;
            var comments = await _context.Comments
    .Where(c => _context.ReviewNotices.Any(n =>
        n.ReviewNoticeID == c.ReviewNoticeID &&
        n.ReviewID == id))
    .ToListAsync();

            ViewBag.ReviewComments = comments;


            return View(review);
        }

    }
}
