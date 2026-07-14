using LostAndFound.DbContexts;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LostAndFound.Controllers
{
    public class UsersController : Controller
    {
        private readonly LostAndFoundDbContext _context;

        public UsersController(LostAndFoundDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);

            var user = await _context.Users
                .Include(u => u.Items)
                .Include(u => u.Claims)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone,
                IsVerified = user.IsVerified,
                CreatedAt = user.CreatedAt,
                Items = user.Items.ToList(),
                Claims = user.Claims.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            var viewModel = new EditProfileViewModel
            {
                FullName = user.FullName,
                Phone = user.Phone
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return Unauthorized();

            int userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            user.FullName = model.FullName;
            user.Phone = model.Phone;

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }
    }
}