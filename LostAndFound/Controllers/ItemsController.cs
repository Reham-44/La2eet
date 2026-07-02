using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var item = context.Items
                .Include(u =>u.User)
                .FirstOrDefault(d=>d.ItemId==id);

            return View(item);
        }
        [HttpGet]
        public IActionResult Create([FromQuery]string? type)
        {
            var item = new Item();

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("lost", StringComparison.OrdinalIgnoreCase))
                {
                    item.Status = ItemType.Lost;
                }
                else if (type.Equals("found", StringComparison.OrdinalIgnoreCase))
                {
                    item.Status = ItemType.Found;
                }
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Item item,ICollection<VerificationQuestion> questions)
        {
            if (item.ImageFile != null) { 
            using (var ms = new MemoryStream()) {
                item.ImageFile.CopyTo(ms);
                string base64 = Convert.ToBase64String(ms.ToArray());
                    item.ImageBase64 = $"data:{item.ImageFile.ContentType};base64,{base64}";
                                   } }
            item.UserId = 11;
            item.ReportStatus = ReportStatus.Pending;
            //if (!ModelState.IsValid)
            //{
            //    return View(item);
            //}
            context.Items.Add(item);
            context.SaveChanges();
            if (item.Status == ItemType.Found)
            {
                foreach (var q in questions)
                {
                    if (string.IsNullOrWhiteSpace(q.QuestionText))
                        continue;

                    q.ItemId = item.ItemId;
                    context.VerificationQuestions.Add(q);
                }

                context.SaveChanges();
            }
            context.SaveChanges();

            return RedirectToAction(nameof(Browse));
        }
    }
}
