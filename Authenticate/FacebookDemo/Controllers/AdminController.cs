using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FacebookDemo.Data;
using FacebookDemo.Model;
using FacebookDemo.Infrastructure;
using System.Security.Claims;

namespace FacebookDemo.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminController(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalSystems = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl,
                ExternalSystems = externalSystems
            });
        }

        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Admin", new { returnUrl });
            var props = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            // Дергает провайдер - facebook
            return Challenge(props, provider);
        }

        /// <summary>
        /// Сюда возвращает провайдер
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if(info == null)
            {
                RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);

            if(result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(RegisterExternal), new RegisterExternalModel
            {
                ReturnUrl = returnUrl,
                // Вытаскиваем данные из провайдера
                UserName = info.Principal.FindFirstValue(ClaimTypes.Name)
            });
        }

        /// <summary>
        /// Форма регистрации после провайдера
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult RegisterExternal(RegisterExternalModel model)
        {
            return View(model);
        }

        /// <summary>
        /// Регистрация после провайдера
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        // Для дублирования экшонов
        [ActionName(nameof(RegisterExternal))]
        public async Task<IActionResult> RegisterExternalConfirmed(RegisterExternalModel model)
        {
            // Так как метод открытый, нужно проверить, что всетаки пользователь прошел  external регистрацию
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                RedirectToAction(nameof (Login));
            }

            var user = new User(model.UserName);

            var result = await _userManager.CreateAsync(user);

            if(result.Succeeded)
            {
                var claimsResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, Policies.AdministratorPolicy));
                if(claimsResult.Succeeded)
                {
                    var loginResult = await _userManager.AddLoginAsync(user, info);
                    if(loginResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return View(loginModel);

            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View(loginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Неверно введен пароль");
                return View(loginModel);
            }

            return Redirect(loginModel.ReturnUrl);
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
