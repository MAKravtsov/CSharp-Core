using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redis.Models;

namespace Redis.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(int id);

        Task Add(Product product);
    }
}