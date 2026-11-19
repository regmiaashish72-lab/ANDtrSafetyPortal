
using ANDtrSafetyPortal.Data;
using ANDtrSafetyPortal.Models;
using Microsoft.AspNetCore.Mvc;
using ANDtrSafetyPortal.Data;
using ANDtrSafetyPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ANDtrSafetyPortal.Controllers
{
    public class SafetyNoticesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SafetyNoticesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var notices = _context.SafetyNotices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                notices = notices.Where(n =>
    n.ProductDescription.ToLower().Contains(search.ToLower()) ||
    n.RecallNumber.ToLower().Contains(search.ToLower()));


            }

            return View(await notices.ToListAsync());
        }
    }
}
