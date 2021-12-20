using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtBearer.Controllers
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
            var userName = "Maksim";
            var userEmail = "Maksim@mail.ru";
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Sub, userEmail)
            };

            var now = System.DateTime.Now;

            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.Secret_key);

            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Constants.Issuer, Constants.Audience, claims,
                notBefore: now, expires: now.AddHours(1), signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            ViewBag.Token = value;

            return View();
        }
    }
}
