using Database.Jwt.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Database.Jwt.Data;
using Database.Jwt.Infrastructure;
using Database.Jwt.Model;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using Database.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace DatabaseюО.Jwt.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ВАЖНО!!!
        // Несколько схем аутентификации через запятую значит, что они будут использоваться через ИЛИ
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ",Identity.Application")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Policies.AdministratorPolicy, AuthenticationSchemes = "Identity.Application")]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Policy = Policies.MangerPolicy, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            if(!ModelState.IsValid)
                return View(loginModel);

            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if(user == null)
            {
                ModelState.AddModelError("", "Не найден пользователь");
                return View(loginModel);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if(!checkPassword)
            {
                ModelState.AddModelError("", "Неверный пароль");
                return View(loginModel);
            }

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, loginModel.UserName),
                new Claim(ClaimTypes.Role, Policies.AdministratorPolicy)
            };

            var now = System.DateTime.Now;

            var key = Encoding.UTF8.GetBytes(Constants.Secret_key);

            var symKey = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(symKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore: now, 
                expires: now.AddHours(1),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // ВАЖНО!!!
            // Добавляем отдельный куки для хранения jwt
            HttpContext.Response.Cookies.Append("token", tokenString, new CookieOptions { HttpOnly = true });

            // ВАЖНО!!!
            // Добавляем куки через Identity - это отличается от обыного куки (наименование схемы - Identity.Application)
            await _signInManager.SignInAsync(user, false);

            return Redirect(loginModel.ReturnUrl)
;        }

        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Redirect("/Home/Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
