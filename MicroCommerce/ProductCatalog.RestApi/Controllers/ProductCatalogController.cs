using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.RestApi.Services.Interfaces;
using ProductCatalog.RestApi.ViewModels;

namespace ProductCatalog.RestApi.Controllers
{
    [ApiController]
    [Route("product-catalog")]
    public class ProductCatalogController : ControllerBase
    {
        private readonly IProductCatalogService _productCatalogService;

        public ProductCatalogController(IProductCatalogService productCatalogService)
        {
            _productCatalogService = productCatalogService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productCatalogService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var product = await _productCatalogService.Get(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductViewModel productViewModel)
        {
            await _productCatalogService.Add(productViewModel);
            return Ok();
        }
    }
}