using Redis.Models;

namespace ProductCatalog.RestApi.ViewModels
{
    public class ProductViewModel
    {
        private int Id { get; }

        private string Name { get; }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
        }

        public Product Product
        {
            get
            {
                var product = new Product(Id, Name);
                return product;
            }
        }
    }
}