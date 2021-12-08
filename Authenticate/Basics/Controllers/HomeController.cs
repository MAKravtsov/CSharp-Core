using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim("MyClaim", "Secret")
            };

            var identity = new ClaimsIdentity(claims, "Identity");

            var user = new ClaimsPrincipal(new[] { identity });

            HttpContext.SignInAsync(user);

            return RedirectToAction("Index");
        }
    }
}
