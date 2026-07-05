using Microsoft.AspNetCore.Mvc;
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
            var items = _context.Items.ToList();
            return View(items);
        }
    }
}