using Microsoft.AspNetCore.Mvc;
using LostAndFound.Enums;
using LostAndFound.DbContexts;

namespace LostAndFound.Controllers
{
    public class HomeController : Controller
    {
        private readonly LostAndFoundDbContext _context;

        public HomeController(LostAndFoundDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Items.
                Where(i=>i.ReportStatus==ReportStatus.Approved && i.User.IsBanned==false).
                OrderByDescending(i=>i.CreatedAt)
                .Take(6)
                .ToList();
            return View(items);
        }
    }
}