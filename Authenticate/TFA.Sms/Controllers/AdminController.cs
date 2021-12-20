using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TFA.Sms.Data;
using TFA.Sms.Infrastructure;
using TFA.Sms.Model;
using TFA.Sms.Services.Interfaces;
using System.Linq;

namespace TFA.Sms.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISmsService _smsService;

        public AdminController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ISmsService smsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smsService = smsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
         * [Authorize(Roles = Policies.AdministratorPolicy)] - legacy
         * Плохо тем, что если много ролей у одного пользователся, то все их придется прописывать в методе Authenticate
         */
        [Authorize(Policy = Policies.AdministratorPolicy)]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Policy = Policies.MangerPolicy)]
        public IActionResult Manager()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View(loginModel);

            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            // Если самостоятельно реализовываем БД
            /*
            var user = _usersContext.Users.SingleOrDefault(y =>
                y.UserName == loginModel.UserName
                && y.Password == y.Password);
            */

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View(loginModel);
            }

            if(user.TwoFactorEnabled)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);

                if (!checkPassword)
                {
                    ModelState.AddModelError("", "Неверно введен пароль");
                    return View(loginModel);
                }

                if (user.PhoneNumber == null)
                {
                    ModelState.AddModelError("", "К аккаунту не привязан номер телефона");
                    return View(loginModel);
                }

                var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
                var sms = $"Your verification code: {code}";
                await _smsService.SendAsync(user.PhoneNumber, sms);

                return RedirectToAction(nameof(SmsTwoFactorAuthorization), new TwoFactorModel
                {
                    ReturnUrl = loginModel.ReturnUrl,
                    PhoneNumber = user.PhoneNumber
                });
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Неверно введен пароль");
                    return View(loginModel);
                }
            }

            return Redirect(loginModel.ReturnUrl);
        }

        [AllowAnonymous]
        public IActionResult SmsTwoFactorAuthorization(TwoFactorModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("SmsTwoFactorAuthorization")]
        public async Task<IActionResult> SmsTwoFactorAuthorizationConfirmed(TwoFactorModel model)
        {
            var user = _userManager.Users.SingleOrDefault(y => y.PhoneNumber == model.PhoneNumber);

            if(user == null)
            {
                ModelState.AddModelError("", $"Пользователей с номером телефона {model.PhoneNumber} больше чем 1");
                return RedirectToAction(nameof(Login));
            }

            var verifySucceded = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, model.Code);

            if (!verifySucceded)
            {
                ModelState.AddModelError("", "Неверно введен код");
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);

            return Redirect(model.ReturnUrl);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
