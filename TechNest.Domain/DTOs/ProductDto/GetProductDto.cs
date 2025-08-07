using TechNest.Domain.DTOs.ImageDto;

namespace TechNest.Domain.DTOs.ProductDto
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public virtual List<ProductImageDto> ProductImages { get; set; }
    }
}
