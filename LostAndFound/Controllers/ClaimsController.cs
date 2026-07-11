using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using LostAndFound.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        private readonly LostAndFoundDbContext _context;
        private readonly UserManager<User> _userManager;

        public ClaimsController(LostAndFoundDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var vm = new ClaimsIndexViewModel
            {
                MyClaims = _context.Claims
                    .Include(c => c.Item)
                    .Where(c => c.UserId == currentUser.Id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToList(),

                IncomingClaims = _context.Claims
                    .Include(c => c.Item)
                    .Include(c => c.User)
                    .Where(c => c.Item.UserId == currentUser.Id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToList()
            };

            return View(vm);
        }

        // GET: /Claims/Create?itemId=5
        public IActionResult Create(int itemId)
        {
            var item = _context.Items
                .Include(i => i.VerificationQuestions)
                .FirstOrDefault(i => i.ItemId == itemId);

            if (item == null) return NotFound();

            ViewBag.Item = item;
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(int itemId)
        {
            var item = _context.Items
                .Include(i => i.VerificationQuestions)
                .FirstOrDefault(i => i.ItemId == itemId);

            if (item == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (item.UserId == currentUser.Id)
            {
                ViewBag.Item = item;
                ViewBag.Error = "لا يمكنك المطالبة بغرض قمت أنت بالإبلاغ عنه";
                return View();
            }
            var alreadyClaimed = _context.Claims.Any(c =>
    c.ItemId == itemId &&
    c.UserId == currentUser.Id);

            if (alreadyClaimed)
            {
                TempData["Error"] = "لقد قمت بإرسال مطالبة لهذا الغرض بالفعل.";
                return RedirectToAction(nameof(Index));
            }

            var answerParts = new System.Collections.Generic.List<string>();
            bool hasEmptyAnswer = false;

            if (item.VerificationQuestions != null && item.VerificationQuestions.Any())
            {
                foreach (var q in item.VerificationQuestions)
                {
                    var answer = Request.Form[$"answer_{q.QId}"].ToString();
                    if (string.IsNullOrWhiteSpace(answer))
                        hasEmptyAnswer = true;

                    answerParts.Add($"{q.QuestionText}: {answer}");
                }
            }
            else
            {
                // لو الغرض مفيهوش أسئلة تحقق، ناخد إجابة عامة بديلة
                var generalAnswer = Request.Form["verificationAnswer"].ToString();
                if (string.IsNullOrWhiteSpace(generalAnswer))
                    hasEmptyAnswer = true;

                answerParts.Add(generalAnswer);
            }

            if (hasEmptyAnswer)
            {
                ViewBag.Item = item;
                ViewBag.Error = "من فضلك جاوب على كل الأسئلة";
                return View();
            }

            var claim = new Claim
            {
                ItemId = itemId,
                UserId = currentUser.Id,
                VerificationAnswer = string.Join(" | ", answerParts),
                ClaimStatus = ClaimStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _context.Claims.Add(claim);
            _context.SaveChanges();

            TempData["Success"] = "تم إرسال مطالبتك بنجاح، هيتم مراجعتها قريبًا";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Claims/Approve/5
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var claim = _context.Claims.Include(c => c.Item).FirstOrDefault(c => c.ClaimId == id);
            if (claim == null || claim.Item.UserId != currentUser.Id) return Forbid();

            claim.ClaimStatus = ClaimStatus.Approved;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Claims/Reject/5
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var claim = _context.Claims.Include(c => c.Item).FirstOrDefault(c => c.ClaimId == id);
            if (claim == null || claim.Item.UserId != currentUser.Id) return Forbid();

            claim.ClaimStatus = ClaimStatus.Rejected;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
