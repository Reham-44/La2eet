using LostAndFound.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        LostAndFoundDbContext context;
        public ItemsController(LostAndFoundDbContext _context)
        {
            context = _context;

        }
        public IActionResult Browse()
        {
            var items = context.Items.ToList();
            return View(items);
        }
        public IActionResult Details(int id)
        {
            var item = context.Items.FirstOrDefault(d=>d.ItemId==id);

            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
