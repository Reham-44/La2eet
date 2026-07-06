using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;

namespace LostAndFound.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly LostAndFoundDbContext _context;

        public AdminController(LostAndFoundDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TotalReports = _context.Items.Count(),

                LostReports = _context.Items.Count(i => i.Status == ItemType.Lost),
                FoundReports = _context.Items.Count(i => i.Status == ItemType.Found),

                ActiveClaims = _context.Claims.Count(),

                RecentItems = _context.Items
                       .OrderByDescending(i => i.CreatedAt)
                       .Take(8)
                       .ToList(),

                CitySummary = _context.Items
                       .GroupBy(i => i.City)
                       .Select(g => new { CityName = g.Key.ToString(), Count = g.Count() })
                       .OrderByDescending(x => x.Count)
                       .Take(6)
                       .ToDictionary(k => k.CityName, v => v.Count)
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ApproveItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null) {
                item.ReportStatus =ReportStatus.Approved;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RejectItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                item.ReportStatus =ReportStatus.Rejected;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult BanUser(int userId) { 
        var user=_context.Users.Find(userId);
            if (user != null) {
                user.IsBanned = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public IActionResult ExportReportsToExcel()
        {
            var items = _context.Items.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("البلاغات");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "رقم البلاغ";
                worksheet.Cell(currentRow, 2).Value = "العنوان";
                worksheet.Cell(currentRow, 3).Value = "النوع";
                worksheet.Cell(currentRow, 4).Value = "المدينة";
                worksheet.Cell(currentRow, 5).Value = "حالة المراجعة";
                worksheet.Cell(currentRow, 6).Value = "تاريخ الإضافة";

                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;

                foreach (var item in items)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.ItemId;
                    worksheet.Cell(currentRow, 2).Value = item.Title;

                    worksheet.Cell(currentRow, 3).Value = item.Status == Enums.ItemType.Lost ? "مفقود" : "موجود";
                    worksheet.Cell(currentRow, 4).Value = item.City.ToString();

                    string statusAr = item.ReportStatus == Enums.ReportStatus.Approved ? "مقبول" :
                                      item.ReportStatus == Enums.ReportStatus.Rejected ? "مرفوض" : "قيد المراجعة";
                    worksheet.Cell(currentRow, 5).Value = statusAr;

                    worksheet.Cell(currentRow, 6).Value = item.CreatedAt.ToString("yyyy-MM-dd");
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    string fileName = $"Reports_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

    }
}

