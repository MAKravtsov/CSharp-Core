using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roles.Infrastructure;
using Roles.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Roles.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
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

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                new Claim(ClaimTypes.Role, loginModel.Role),
                new Claim(CustomClaims.SecretWord, loginModel.SecretWord)
            };

            // CookieAuthName - очень важно, без него не заработает
            var identity = new ClaimsIdentity(claims, Startup.CookieAuthName);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return Redirect(loginModel.ReturnUrl);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
