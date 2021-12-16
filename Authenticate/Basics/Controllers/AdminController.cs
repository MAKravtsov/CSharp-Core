using Basics.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
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
        public async Task<IActionResult> Login( LoginModel loginModel )
        {
            if (!ModelState.IsValid)
                return View(loginModel);

            var claims = new List<Claim>()
            {
                new Claim("Demo", "Value")
            };

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
