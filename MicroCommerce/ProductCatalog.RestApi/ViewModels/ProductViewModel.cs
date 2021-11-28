using Redis.Models;

namespace ProductCatalog.RestApi.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}