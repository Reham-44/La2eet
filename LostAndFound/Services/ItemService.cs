using LostAndFound.Enums;
using LostAndFound.Models;
using LostAndFound.Models.ViewModels;
using LostAndFound.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Services
{
    public class ItemFilter
    {
        public string? Search { get; set; }
        public ItemType? StatusFilter { get; set; }
        public City? CityFilter { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class CreateItemResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class ItemService
    {
        private readonly ItemRepository itemRepository;
        private readonly UserManager<User> userManager;

        public ItemService(ItemRepository _itemRepository, UserManager<User> _userManager)
        {
            itemRepository = _itemRepository;
            userManager = _userManager;
        }

        public List<Item> Browse(ItemFilter filter)
        {
            var query = itemRepository.GetApprovedItemsQuery();

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => i.Title.Contains(filter.Search) || i.Description.Contains(filter.Search));

            if (filter.StatusFilter.HasValue)
                query = query.Where(i => i.Status == filter.StatusFilter.Value);

            if (filter.CityFilter.HasValue)
                query = query.Where(i => i.City == filter.CityFilter.Value);

            if (filter.DateFrom.HasValue)
            {
                var from = DateOnly.FromDateTime(filter.DateFrom.Value);
                query = query.Where(i => i.LostOrFoundDate >= from);
            }

            if (filter.DateTo.HasValue)
            {
                var to = DateOnly.FromDateTime(filter.DateTo.Value);
                query = query.Where(i => i.LostOrFoundDate <= to);
            }

            return query.OrderByDescending(i => i.CreatedAt).ToList();
        }

        public Item? GetItemDetails(int id)
        {
            return itemRepository.GetById(id, includeUser: true);
        }

        public async Task<bool> IsAlreadyClaimedByCurrentUserAsync(int itemId, System.Security.Claims.ClaimsPrincipal principal)
        {
            if (principal.Identity == null || !principal.Identity.IsAuthenticated)
                return false;

            var currentUser = await userManager.GetUserAsync(principal);
            if (currentUser == null)
                return false;

            return itemRepository.HasUserClaimedItem(itemId, currentUser.Id);
        }

        // بقت الميثود دي بتستقبل ViewModel مش Item مباشرة
        public CreateItemResult CreateItem(ItemViewModel model, int userId)
        {
            List<VerificationQuestion> validQuestions = new();

            if (model.Status == ItemType.Found)
            {
                validQuestions = model.Questions
                    .Where(q => !string.IsNullOrWhiteSpace(q.QuestionText) && !string.IsNullOrWhiteSpace(q.ExpectedAnswerHash))
                    .Select(q => new VerificationQuestion
                    {
                        QuestionText = q.QuestionText!,
                        ExpectedAnswerHash = q.ExpectedAnswerHash!
                    })
                    .ToList();

                if (validQuestions.Count == 0)
                {
                    return new CreateItemResult
                    {
                        Success = false,
                        ErrorMessage = "يجب إضافة سؤال تحقق واحد على الأقل."
                    };
                }
            }

            // تحويل الـ ViewModel لـ Entity
            var item = new Item
            {
                Title = model.Title,
                Description = model.Description,
                City = model.City,
                Location = model.Location,
                LostOrFoundDate = model.LostOrFoundDate!.Value,
                Status = model.Status,
                UserId = userId,
                ReportStatus = ReportStatus.Pending
            };

            if (model.ImageFile != null)
            {
                using var ms = new MemoryStream();
                model.ImageFile.CopyTo(ms);
                string base64 = Convert.ToBase64String(ms.ToArray());
                item.ImageBase64 = $"data:{model.ImageFile.ContentType};base64,{base64}";
            }

            itemRepository.Add(item);
            itemRepository.SaveChanges();

            if (item.Status == ItemType.Found)
            {
                foreach (var q in validQuestions)
                    q.ItemId = item.ItemId;

                itemRepository.AddVerificationQuestions(validQuestions);
                itemRepository.SaveChanges();
            }

            return new CreateItemResult { Success = true };
        }
    }
}