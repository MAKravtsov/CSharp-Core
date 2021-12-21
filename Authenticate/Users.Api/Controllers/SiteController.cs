using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Users.Api.Controllers
{
    public class SiteController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public SiteController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrdersAsync()
        {
            // retrieve to Identity Server
            var authClient = _factory.CreateClient();
            // /.well-knowm/openid-configuration
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:7001"); // собирает инфо с IdentityServer

            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "OrdersAPI"
            });


            // retrieve to Orders
            var ordersClient = _factory.CreateClient();

            // Передаем токен
            ordersClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await ordersClient.GetAsync("https://localhost:5001/site/secret");

            if(!response.IsSuccessStatusCode)
            {
                ViewBag.Message = response.StatusCode.ToString();
            }
            else
            {
                ViewBag.Message = await response.Content.ReadAsStringAsync();
            }

            return View();
        }
    }
}
