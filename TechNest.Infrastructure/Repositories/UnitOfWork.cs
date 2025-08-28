using AutoMapper;
using TechNest.Domain.Interfaces;
using TechNest.Domain.Services;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IImageManagementService _imageManagementService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IImageManagementService imageManagementService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _imageManagementService = imageManagementService;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context,mapper,imageManagementService);
            ProductImageRepository = new ProductImageRepository(_context);       
        }

        public async Task<bool> SaveChangesAsync()
        {
              await _context.SaveChangesAsync();
              return true;

        }
    }
}
