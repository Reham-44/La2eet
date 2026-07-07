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
    }
}