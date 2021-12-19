using Database.Entities;
using Database.Infrastructure;
using Database.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Database.Data;

namespace Database.Controllers
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

            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

            if(!result.Succeeded)
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
