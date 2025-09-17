using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Entites.Product;
using TechNest.Domain.Sharing;

namespace TechNest.Domain.Interfaces
{
    public interface IProductRepository: IRepositories<Product>
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(CreateProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
    }
}
