using AutoMapper;
using BabyBackend.Migrations;
using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Mapper
{
    public class BabyMapper : Profile
    {
        public BabyMapper()
        {
            CreateMap<Users, UserRegisterDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap< UserViewDto ,Users> ().ReverseMap();
            CreateMap<CategoryViewDto, Category>().ReverseMap();
            CreateMap<WhishList, WhishListDto>().ReverseMap();
        }
    }
}
