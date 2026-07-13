using LostAndFound.Models;
using LostAndFound.Models.ViewModels;

namespace LostAndFound.Services
{
    public enum ClaimActionResult
    {
        Success,
        NotFound,
        Forbidden
    }

    public class ClaimCreateResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public interface IClaimsService
    {
        Task<ClaimsIndexViewModel> GetClaimsForUserAsync(int userId);
        Task<Item?> GetItemForClaimCreationAsync(int itemId);

        Task<ClaimCreateResult> CreateClaimAsync(
            int itemId,
            int currentUserId,
            Dictionary<int, string> questionAnswers,
            string? generalAnswer);

        Task<ClaimActionResult> ApproveClaimAsync(int claimId, int currentUserId);
        Task<ClaimActionResult> RejectClaimAsync(int claimId, int currentUserId);
    }
}
