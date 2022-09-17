using Library.Data.Models;

namespace Library.Domain.Catalog.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Book>> GetCatalog();
    }
}
