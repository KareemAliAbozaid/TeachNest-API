using TechNest.Domain.Entites.Product;
using TechNest.Domain.Interfaces;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class CategoryRepository : Repositories<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
