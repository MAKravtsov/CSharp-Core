using IdentityServer.Data;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManger, SignInManager<User> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManger.FindByNameAsync(model.UserName);

            if(user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Password is nor correct");
                return View(model);
            }

            return Redirect(model.ReturnUrl);
        }
    }
}
