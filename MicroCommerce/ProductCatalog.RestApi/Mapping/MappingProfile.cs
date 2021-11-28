using AutoMapper;
using ProductCatalog.RestApi.ViewModels;
using Redis.Models;

namespace ProductCatalog.RestApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
        }
    }
}