
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ANDtrSafetyPortal.Data;
using ANDtrSafetyPortal.Models;

namespace ANDtrSafetyPortal.Controllers
{
    public class RiskReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.RiskReviews.ToListAsync();
            return View(reviews);
        }
        // Display the Create Risk Review page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RiskReview review)
        {
            if (string.IsNullOrWhiteSpace(review.Title))
            {
                ModelState.AddModelError("Title", "Please enter a review title.");
            }

            if (!ModelState.IsValid)
            {
                return View(review);
            }

            // Temporary employee ID for testing.
            // Replace with the logged-in employee's ID later.
            review.EmployeeID = 1;

            _context.RiskReviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }

}
