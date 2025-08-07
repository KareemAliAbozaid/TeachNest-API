using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Entites.Product;

namespace TechNest.Domain.Interfaces
{
    public interface IProductRepository: IRepositories<Product>
    {
        Task<bool> AddAsync(CreateProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
    }
}
