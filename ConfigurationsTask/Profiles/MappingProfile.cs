using AutoMapper;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Auth;
using ConfigurationsTask.Entities.Dtos.Auth;
using ConfigurationsTask.Entities.Dtos.Brands;
using ConfigurationsTask.Entities.Dtos.Products;

namespace ConfigurationsTask.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();
        CreateMap<Brand, GetBrandDto>();
        CreateMap<DeleteBrandDto, Brand>();

        CreateMap<Product, GetProductDto>();
       CreateMap<CreateProductDto, Product>();
       CreateMap<UpdateProductDto, Product>();
       
       CreateMap<RegisterDto, AppUser>().ReverseMap();
    }
}