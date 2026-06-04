using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
