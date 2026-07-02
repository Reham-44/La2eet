using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
