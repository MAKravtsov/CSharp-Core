using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.RestApi.Services.Interfaces;
using ProductCatalog.RestApi.ViewModels;
using Redis.Repositories.Interfaces;

namespace ProductCatalog.RestApi.Services.Implementations
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductRepository _productRepository;

        public ProductCatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var productViewModels = products.Select(y => new ProductViewModel(y));
            return productViewModels;
        }

        public async Task<ProductViewModel> Get(int id)
        {
            var product = await _productRepository.GetById(id);
            var productViewModel = new ProductViewModel(product);
            return productViewModel;
        }

        public async Task Add(ProductViewModel productViewModel)
        {
            var product = productViewModel.Product;
            await _productRepository.Add(product);
        }
    }
}