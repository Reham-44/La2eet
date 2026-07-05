using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    //[Authorize(Roles="ADMIN")]
    public class AdminController : Controller
    {
        private readonly LostAndFoundDbContext _context;

        public AdminController(LostAndFoundDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalReports = _context.Items.Count(),

                LostReports = _context.Items.Count(i => i.Status == ItemType.Lost),
                FoundReports = _context.Items.Count(i => i.Status == ItemType.Found),

                ActiveClaims = _context.Claims.Count(),

                RecentItems = _context.Items
                       .OrderByDescending(i => i.CreatedAt)
                       .Take(8)
                       .ToList(),

                CitySummary = _context.Items
                       .GroupBy(i => i.City)
                       .Select(g => new { CityName = g.Key.ToString(), Count = g.Count() })
                       .OrderByDescending(x => x.Count)
                       .Take(6)
                       .ToDictionary(k => k.CityName, v => v.Count)
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ApproveItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null) {
                item.ReportStatus =ReportStatus.Approved;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RejectItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                item.ReportStatus =ReportStatus.Rejected;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}

