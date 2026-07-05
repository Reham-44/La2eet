using LostAndFound.DbContexts;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Enums;

namespace LostAndFound.Controllers
{
    public class HomeController : Controller
    {       
               LostAndFoundDbContext context;
 public HomeController(LostAndFoundDbContext _context)
        {
            context = _context;

        }
        public IActionResult Index()
        {
            var items = context.Items.
                Where(i=>i.ReportStatus==ReportStatus.Approved).
                OrderByDescending(i=>i.CreatedAt)
                .Take(6)
                .ToList();
            return View(items);
        }

    }
}
