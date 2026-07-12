using LostAndFound.Enums;
using LostAndFound.Models.ViewModels;
using LostAndFound.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemService itemService;

        public ItemsController(ItemService _itemService)
        {
            itemService = _itemService;
        }

        public IActionResult Browse(string? search, ItemType? statusFilter, City? cityFilter, DateTime? dateFrom, DateTime? dateTo)
        {
            var filter = new ItemFilter
            {
                Search = search,
                StatusFilter = statusFilter,
                CityFilter = cityFilter,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            return View(itemService.Browse(filter));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = itemService.GetItemDetails(id);
            if (item == null)
                return NotFound();

            ViewBag.AlreadyClaimed = await itemService.IsAlreadyClaimedByCurrentUserAsync(id, User);

            return View(item);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create([FromQuery] string? type)
        {
            var model = new ItemViewModel();

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("lost", StringComparison.OrdinalIgnoreCase))
                    model.Status = ItemType.Lost;
                else if (type.Equals("found", StringComparison.OrdinalIgnoreCase))
                    model.Status = ItemType.Found;
            }
            model.Questions.Add(new VerificationQuestionViewModel());

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(userIdClaim!);

            var result = itemService.CreateItem(model, userId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage!);
                return View(model);
            }

            return RedirectToAction(nameof(Browse));
        }
    }
}