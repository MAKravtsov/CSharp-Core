using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProductCatalog.RestApi.Services.Interfaces;
using ProductCatalog.RestApi.ViewModels;
using Redis.Models;
using Redis.Repositories.Interfaces;

namespace ProductCatalog.RestApi.Services.Implementations
{
    public class ProductCatalogService : IProductCatalogService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductCatalogService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var productViewModels = products
                .Select(y => _mapper.Map<Product, ProductViewModel>(y));
            return productViewModels;
        }

        public async Task<ProductViewModel> Get(int id)
        {
            var product = await _productRepository.GetById(id);
            var productViewModel = _mapper.Map<Product, ProductViewModel>(product);
            return productViewModel;
        }

        public async Task Add(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            await _productRepository.Add(product);
        }
    }
}