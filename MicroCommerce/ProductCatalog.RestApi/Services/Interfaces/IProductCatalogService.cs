using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.RestApi.ViewModels;

namespace ProductCatalog.RestApi.Services.Interfaces
{
    public interface IProductCatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task<ProductViewModel> Get(int id);

        Task Add(ProductViewModel productViewModel);
    }
}