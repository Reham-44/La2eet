using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Profile(int id)
        {
            return View();
        }
    }
}
