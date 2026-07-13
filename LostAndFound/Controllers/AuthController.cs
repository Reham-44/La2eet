using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using LostAndFound.Services;
namespace LostAndFound.Controllers
{
    public class AuthController : Controller
    {
        //   private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;

        // public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        // {
        //    _userManager = userManager;
        //  _signInManager = signInManager;
        //  }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // اتأكد الأول إن الحساب مش محظور قبل ما نحاول نعمله Sign In
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null && !existingUser.EmailConfirmed)
            {
                ModelState.AddModelError("", "من فضلك أكّد البريد الإلكتروني الأول");
                return View(model);
            }
            if (existingUser != null && existingUser.IsBanned)
            {
                ModelState.AddModelError(string.Empty, "تم حظر هذا الحساب، تواصل مع الدعم لمزيد من التفاصيل");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "البريد الإلكتروني أو كلمة المرور غير صحيحة");
                return View(model);
            }

            // نجيب اليوزر عشان نعرف الـ Role بتاعه ونوجهه صح
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && user.Role == Role.ADMIN)
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Phone = model.Phone,
                EmailConfirmed = false,
                IsVerified = false,
                Role = Role.USER
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }






            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action(
                "ConfirmEmail",
                "Auth",
                new
                {
                    userId = user.Id,
                    token = token
                },
                Request.Scheme);


            await _emailService.SendEmailAsync(
                user.Email,
                "تأكيد البريد الإلكتروني",
                $@"
    <h2>Lost & Found</h2>
    <p>اضغط على الرابط التالي لتأكيد بريدك الإلكتروني:</p>
    <a href='{confirmationLink}'>تأكيد الحساب</a>
    "
            );

            TempData["SuccessMessage"] = "تم إنشاء الحساب بنجاح. تم إرسال رابط تأكيد إلى بريدك الإلكتروني.";
            return RedirectToAction("Login");
            // return RedirectToAction("Login");
        }

            // POST: /Auth/Logout
            [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        // POST: /Auth/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            // ملاحظة أمنية: منقولش "الإيميل مش موجود" عشان محدش يقدر يتأكد
            // مين اليوزرز المسجلين عن طريق تجربة إيميلات عشوائية
            if (user == null)
            {
                ViewBag.Message = "لو الإيميل ده مسجل عندنا، هيوصلك رابط إعادة تعيين كلمة المرور.";
                return View("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Auth",
                new { email = model.Email, token = token }, protocol: Request.Scheme);

            // TODO: لما يبقى عندك خدمة إيميل حقيقية (SMTP/SendGrid)،
            // ابعت resetLink بدل ما تعرضه على الشاشة، وامسح الـ ViewBag.ResetLink من View التأكيد
            // ViewBag.ResetLink = resetLink;
            await _emailService.SendEmailAsync(
     model.Email,
     "إعادة تعيين كلمة المرور",
     $@"
    <h2>Lost & Found</h2>
    <p>اضغطي على الرابط التالي لإعادة تعيين كلمة المرور:</p>
    <a href='{resetLink}'>إعادة تعيين كلمة المرور</a>
    ");
            return View("ForgotPasswordConfirmation");
        }

        // GET: /Auth/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return RedirectToAction(nameof(Login));

            var model = new ResetPasswordViewModel { Email = email, Token = token };
            return View(model);
        }

        // POST: /Auth/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // منورّيش السبب الحقيقي، بس نوجهه كإنه نجح عشان أمان الحسابات
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        [HttpGet]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse));

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                GoogleDefaults.AuthenticationScheme,
                redirectUrl);

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return RedirectToAction("Login");


            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return RedirectToAction("Login");


            var result = await _userManager.ConfirmEmailAsync(user, token);


            if (result.Succeeded)
            {
                user.IsVerified = true;
                user.EmailConfirmed = true;

                await _userManager.UpdateAsync(user);

                return View("EmailConfirmed");
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                return RedirectToAction(nameof(Login));

           

            // لو الحساب مربوط بجوجل بالفعل
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            // أول مرة يسجل بجوجل
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return RedirectToAction(nameof(Login));

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "",
                    Role = Role.USER,
                    IsVerified = true
                };

                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    return RedirectToAction(nameof(Login));
            }

            // اربط حساب جوجل بالحساب
            await _userManager.AddLoginAsync(user, info);

            // اعمل Login
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }





    }
}
