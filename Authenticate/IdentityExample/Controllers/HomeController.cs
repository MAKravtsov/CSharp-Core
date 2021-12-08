using IdentityExample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManeger;

        public HomeController(
            UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManeger = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // login functionality

            var user = await _userManeger.FindByNameAsync(username);

            if(user == null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // register functionality

            var user = new IdentityUser
            {
                UserName = username,
                Email = ""
            };

            var result = await _userManeger.CreateAsync(user, password);

            if(result.Succeeded)
            {
                // log in user

                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (!signInResult.Succeeded)
                {
                    throw new System.Exception("Невозможно зарегестрировать пользователя");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
