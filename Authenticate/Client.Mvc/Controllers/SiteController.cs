using Client.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.Mvc.Controllers
{
    public class SiteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SiteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var model = new ClaimManager(HttpContext, User);

            // Обращение к другому сервису с Bearer токеном
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.AccessToken);
                var ordersApiSecret = await client.GetStringAsync("https://localhost:5001/site/secret");
                ViewBag.Message = ordersApiSecret;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(model);
        }

        [Authorize(Policy = "HasDateOfBirth")]
        public async Task<IActionResult> Secret1()
        {
            var model = new ClaimManager(HttpContext, User);

            return View(model);
        }

        [Authorize(Policy = "OlderThan10")]
        public async Task<IActionResult> Secret2()
        {
            var model = new ClaimManager(HttpContext, User);

            return View(model);
        }
    }
}
