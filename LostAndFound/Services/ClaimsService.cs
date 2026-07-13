using LostAndFound.Enums;
using LostAndFound.Models;
using LostAndFound.Models.ViewModels;
using LostAndFound.Repositories;

namespace LostAndFound.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClaimsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClaimsIndexViewModel> GetClaimsForUserAsync(int userId)
        {
            var myClaims = await _unitOfWork.Claims.FindAsync(
                c => c.UserId == userId,
                c => c.Item);

            var incomingClaims = await _unitOfWork.Claims.FindAsync(
                c => c.Item.UserId == userId,
                c => c.Item,
                c => c.User);

            return new ClaimsIndexViewModel
            {
                MyClaims = myClaims.OrderByDescending(c => c.CreatedAt).ToList(),
                IncomingClaims = incomingClaims.OrderByDescending(c => c.CreatedAt).ToList()
            };
        }

        public async Task<Item?> GetItemForClaimCreationAsync(int itemId)
        {
            var items = await _unitOfWork.Items.FindAsync(
                i => i.ItemId == itemId,
                i => i.VerificationQuestions);

            return items.FirstOrDefault();
        }

        public async Task<ClaimCreateResult> CreateClaimAsync(
            int itemId,
            int currentUserId,
            Dictionary<int, string> questionAnswers,
            string? generalAnswer)
        {
            var item = await GetItemForClaimCreationAsync(itemId);

            if (item == null)
            {
                return new ClaimCreateResult { Success = false, ErrorMessage = "الغرض غير موجود" };
            }

            if (item.UserId == currentUserId)
            {
                return new ClaimCreateResult { Success = false, ErrorMessage = "لا يمكنك المطالبة بغرض قمت أنت بالإبلاغ عنه" };
            }

            var answerParts = new List<string>();

            if (item.VerificationQuestions != null && item.VerificationQuestions.Any())
            {
                foreach (var q in item.VerificationQuestions)
                {
                    if (!questionAnswers.TryGetValue(q.QId, out var answer) || string.IsNullOrWhiteSpace(answer))
                    {
                        return new ClaimCreateResult { Success = false, ErrorMessage = "من فضلك جاوب على كل الأسئلة" };
                    }

                    answerParts.Add($"{q.QuestionText}: {answer}");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(generalAnswer))
                {
                    return new ClaimCreateResult { Success = false, ErrorMessage = "من فضلك جاوب على سؤال التحقق" };
                }

                answerParts.Add(generalAnswer);
            }

            var claim = new Claim
            {
                ItemId = itemId,
                UserId = currentUserId,
                VerificationAnswer = string.Join(" | ", answerParts),
                ClaimStatus = ClaimStatus.Pending,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Claims.AddAsync(claim);
            await _unitOfWork.CompleteAsync();

            return new ClaimCreateResult { Success = true };
        }

        public async Task<ClaimActionResult> ApproveClaimAsync(int claimId, int currentUserId)
        {
            return await UpdateClaimStatusAsync(claimId, currentUserId, ClaimStatus.Approved);
        }

        public async Task<ClaimActionResult> RejectClaimAsync(int claimId, int currentUserId)
        {
            return await UpdateClaimStatusAsync(claimId, currentUserId, ClaimStatus.Rejected);
        }

        private async Task<ClaimActionResult> UpdateClaimStatusAsync(int claimId, int currentUserId, ClaimStatus newStatus)
        {
            var claims = await _unitOfWork.Claims.FindAsync(
                c => c.ClaimId == claimId,
                c => c.Item);

            var claim = claims.FirstOrDefault();

            if (claim == null)
            {
                return ClaimActionResult.NotFound;
            }

            if (claim.Item.UserId != currentUserId)
            {
                return ClaimActionResult.Forbidden;
            }

            claim.ClaimStatus = newStatus;
            _unitOfWork.Claims.Update(claim);
            await _unitOfWork.CompleteAsync();

            return ClaimActionResult.Success;
        }
    }
}
