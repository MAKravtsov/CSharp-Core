using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redis.Models;
using Redis.Repositories.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Redis.Repositories.Implementations
{
    public class ProductsRepository : BaseRepository, IProductRepository
    {
        public ProductsRepository() : base(RepositoriesEnum.Products) { }
        
        public async Task<IEnumerable<Product>> GetAll()
        {
            var productsHash = await RedisRepo.HashGetAllAsync(RepositoryName);
            var products = productsHash
                .Select(y => JsonConvert.DeserializeObject<Product>(y.Value));
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var redisValue = await RedisRepo.HashGetAsync(RepositoryName, id.ToString().ToUpper());
            var product = JsonConvert.DeserializeObject<Product>(redisValue);
            return product;
        }

        public async Task Add(Product product)
        {
            var hashEntry = product.HashEntry;
            await RedisRepo.HashSetAsync(RepositoryName, new[] {hashEntry});
        }
    }
}