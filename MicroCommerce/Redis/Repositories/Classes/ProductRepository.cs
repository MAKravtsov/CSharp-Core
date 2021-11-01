using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Redis.Repositories.Interfaces;

namespace Redis.Repositories.Classes
{
    public class ProductRepository : Repository, IProductRepository
    {
        public async Task<IEnumerable<Product>> Get()
        {
            var productsHash = await RedisRepo.HashGetAllAsync("Products");
            var products = productsHash
                .Select(y => Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(y.Value));
            return products;
        }

        public async Task<Product> GetById(Guid id)
        {
            var redisValue = await RedisRepo.HashGetAsync("Products", id.ToString().ToUpper());
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(redisValue);
            return product;
        }

        public ProductRepository() : base(0)
        {
        }
    }
}