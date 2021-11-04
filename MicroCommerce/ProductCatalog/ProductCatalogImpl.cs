using Interfaces.Interfaces;
using Interfaces.Models;
using System.Collections.Generic;
using Redis.Repositories.Interfaces;

namespace ProductCatalog
{
    public class ProductCatalogImpl : IProductCatalog
    {
        private readonly IProductRepository _productRepository;
        public ProductCatalogImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> Get()
        {
            var products = _productRepository.Get().Result;
            return products;
        }

        public Product Get(int productId)
        {
            var products = _productRepository.GetById(productId).Result;
            return products;
        }
    }
}
