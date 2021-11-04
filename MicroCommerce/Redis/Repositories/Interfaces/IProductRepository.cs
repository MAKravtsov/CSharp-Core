using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Redis.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Get();

        Task<Product> GetById(int id);
    }
}