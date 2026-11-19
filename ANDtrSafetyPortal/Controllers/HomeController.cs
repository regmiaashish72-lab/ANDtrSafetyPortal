using ANDtrSafetyPortal.Data;
using ANDtrSafetyPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ANDtrSafetyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult CheckEmployee()
        {
            var employee = _context.Users.FirstOrDefault(u => u.UserID == 1);

            if (employee == null)
                return Content("User ID 1 does not exist.");

            return Content($"User found: {employee.FullName}, Role: {employee.Role}");
        }
        [HttpGet]
        public async Task<IActionResult> CreateTestEmployee()
        {
            var employee = await _context.Users.FindAsync(1);

            if (employee != null)
            {
                return Content("Employee ID 1 already exists.");
            }

            var newEmployee = new User
            {
                UserID = 1,
                FullName = "Test Employee",
                Email = "employee@andtr.test",
                Role = "Employee"
            };

            _context.Users.Add(newEmployee);
            await _context.SaveChangesAsync();

            return Content("Test employee created successfully!");
        }
        [HttpGet]
        public async Task<IActionResult> LinkTestNotice()
        {
            var review = await _context.RiskReviews.FindAsync(1);

            var notice = _context.SafetyNotices
                .FirstOrDefault(n => n.RecallNumber == "TEST-001");

            if (review == null || notice == null)
                return Content("Review or notice not found.");

            var exists = _context.ReviewNotices.Any(r =>
                r.ReviewID == review.ReviewID &&
                r.RecallNumber == notice.RecallNumber);

            if (exists)
                return Content("Notice already linked!");

            _context.ReviewNotices.Add(new ReviewNotice
            {
                ReviewID = review.ReviewID,
                RecallNumber = notice.RecallNumber
            });

            await _context.SaveChangesAsync();

            return Content("Recall notice linked to risk review successfully!");
        }
        [HttpGet]
        public async Task<IActionResult> CreateTestCustomer()
        {
            var existing = _context.Users
                .FirstOrDefault(u => u.Email == "customer@andtr.test");

            if (existing != null)
                return Content($"Test customer already exists. ID: {existing.UserID}");

            var customer = new User
            {
                FullName = "Test Customer",
                Email = "customer@andtr.test",
                Role = "Customer"
            };

            _context.Users.Add(customer);
            await _context.SaveChangesAsync();

            return Content($"Test customer created! ID: {customer.UserID}");
        }
        [HttpGet]
        public async Task<IActionResult> ShareTestReview()
        {
            var review = await _context.RiskReviews.FindAsync(1);
            var customer = await _context.Users.FindAsync(2);

            if (review == null || customer == null)
                return Content("Review or customer not found.");

            if (customer.Role != "Customer")
                return Content("User 2 is not a customer.");

            var alreadyShared = _context.SharedFindings.Any(s =>
                s.ReviewID == 1 && s.CustomerID == 2);

            if (alreadyShared)
                return Content("Review already shared with customer!");

            _context.SharedFindings.Add(new SharedFinding
            {
                ReviewID = 1,
                CustomerID = 2,
                SharedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return Content("Risk Review 1 shared with Customer 2 successfully!");
        }
        [HttpGet]
        public async Task<IActionResult> CreateTestComment()
        {
            var comment = new Comment
            {
                ReviewNoticeID = 1,
                UserID = 2,
                CommentText = "Please check this device recall."
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Content("Test comment saved successfully!");
        }

    }
}
