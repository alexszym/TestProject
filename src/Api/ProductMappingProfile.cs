using Api.Data;
using Api.Models;
using AutoMapper;

namespace Api
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}
