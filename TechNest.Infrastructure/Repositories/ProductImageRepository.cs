using TechNest.Domain.Entites.Product;
using TechNest.Domain.Interfaces;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class ProductImageRepository: Repositories<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
