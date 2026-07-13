using LostAndFound.Models;

namespace LostAndFound.Services
{
    public enum LoginResultType
    {
        Success,
        InvalidCredentials,
        Banned
    }

    public class LoginServiceResult
    {
        public LoginResultType ResultType { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class RegisterServiceResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class ResetPasswordServiceResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public interface IAuthService
    {
        Task<LoginServiceResult> LoginAsync(string email, string password, bool rememberMe);
        Task<RegisterServiceResult> RegisterAsync(string fullName, string email, string phone, string password);
        Task LogoutAsync();
        Task<string?> GeneratePasswordResetLinkAsync(string email, Func<string, string, string> buildLinkCallback);

        Task<ResetPasswordServiceResult> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
