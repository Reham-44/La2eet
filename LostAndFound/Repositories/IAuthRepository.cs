using LostAndFound.Models;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(User user, string password);

        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure);
        Task SignInAsync(User user, bool isPersistent);
        Task SignOutAsync();

        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
