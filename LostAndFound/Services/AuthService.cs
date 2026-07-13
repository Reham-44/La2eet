using LostAndFound.Enums;
using LostAndFound.Models;
using LostAndFound.Repositories;

namespace LostAndFound.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEmailSender _emailSender;

        public AuthService(IAuthRepository authRepository, IEmailSender emailSender)
        {
            _authRepository = authRepository;
            _emailSender = emailSender;
        }

        public async Task<LoginServiceResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _authRepository.FindByEmailAsync(email);

            if (user != null && user.IsBanned)
            {
                return new LoginServiceResult { ResultType = LoginResultType.Banned };
            }

            var result = await _authRepository.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new LoginServiceResult { ResultType = LoginResultType.InvalidCredentials };
            }

            return new LoginServiceResult
            {
                ResultType = LoginResultType.Success,
                IsAdmin = user != null && user.Role == Role.ADMIN
            };
        }

        public async Task<RegisterServiceResult> RegisterAsync(string fullName, string email, string phone, string password)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                Phone = phone,
                IsVerified = false,
                Role = Role.USER 
            };

            var result = await _authRepository.CreateUserAsync(user, password);

            if (!result.Succeeded)
            {
                return new RegisterServiceResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await _authRepository.SignInAsync(user, isPersistent: false);

            return new RegisterServiceResult { Success = true };
        }

        public async Task LogoutAsync()
        {
            await _authRepository.SignOutAsync();
        }

        public async Task<string?> GeneratePasswordResetLinkAsync(string email, Func<string, string, string> buildLinkCallback)
        {
            var user = await _authRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var token = await _authRepository.GeneratePasswordResetTokenAsync(user);
            var link = buildLinkCallback(email, token);

            var emailBody = $@"
                <div style='font-family:Cairo,Arial,sans-serif;direction:rtl;text-align:right;padding:20px'>
                    <h2>إعادة تعيين كلمة المرور</h2>
                    <p>وصلنا طلب لإعادة تعيين كلمة المرور بتاعت حسابك في منصة لقيت.</p>
                    <p>لو أنت اللي طلبت ده، دوس على اللينك ده:</p>
                    <p><a href='{link}' style='background:#1a56db;color:#fff;padding:10px 20px;border-radius:6px;text-decoration:none'>إعادة تعيين كلمة المرور</a></p>
                    <p style='color:#888;font-size:0.85rem'>لو محاولتش تغيير كلمة المرور، تجاهل الإيميل ده بأمان.</p>
                </div>";

            await _emailSender.SendEmailAsync(email, "إعادة تعيين كلمة المرور - لقيت", emailBody);

            return link;
        }

        public async Task<ResetPasswordServiceResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _authRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return new ResetPasswordServiceResult { Success = true };
            }

            var result = await _authRepository.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return new ResetPasswordServiceResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new ResetPasswordServiceResult { Success = true };
        }
    }
}
