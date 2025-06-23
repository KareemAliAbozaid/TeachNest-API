using TechNest.Domain.Entites.Product;
using TechNest.Domain.Interfaces;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class ProductRepository : Repositories<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
