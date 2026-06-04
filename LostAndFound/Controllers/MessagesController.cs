using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
