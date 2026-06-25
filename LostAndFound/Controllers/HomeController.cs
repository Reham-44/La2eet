using LostAndFound.DbContexts;
using Microsoft.AspNetCore.Mvc;

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
            var items = context.Items.ToList();
            return View(items);
        }

    }
}
