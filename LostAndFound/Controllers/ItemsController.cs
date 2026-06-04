using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Browse(string q)
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
