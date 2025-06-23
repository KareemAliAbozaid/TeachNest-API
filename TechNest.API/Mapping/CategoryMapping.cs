using AutoMapper;
using TechNest.Domain.DTOs.CategoriesDto;
using TechNest.Domain.Entites.Product;

namespace TechNest.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CreateCategoryDto,Category>().ReverseMap();
            CreateMap<UpdateCategoryDto,Category>().ReverseMap();
        }
    }
}
