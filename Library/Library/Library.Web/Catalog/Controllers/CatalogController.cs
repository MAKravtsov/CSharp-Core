using Library.Domain.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Catalog.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _catalogService.GetCatalog();
            return Ok(books);
        }
    }
}
