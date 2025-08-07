using AutoMapper;
using TechNest.Domain.DTOs.ImageDto;
using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Entites.Product;

namespace TechNest.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping() 
        { 
            CreateMap<CreateProductDto,Product>().ForMember(x=>x.ProductImages,op=>op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDto,Product>().ForMember(x=>x.ProductImages,op=>op.Ignore()).ReverseMap();
            CreateMap<Product,GetProductDto>().ForMember(x=>x.CategoryName, op => op.MapFrom(src => src.Category.Name)).ReverseMap();

            CreateMap<ProductImage,ProductImageDto>().ReverseMap(); 
        }
    }
}
