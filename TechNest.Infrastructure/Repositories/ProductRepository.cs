using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Entites.Product;
using TechNest.Domain.Interfaces;
using TechNest.Domain.Services;
using TechNest.Infrastructure.Data;

namespace TechNest.Infrastructure.Repositories
{
    public class ProductRepository : Repositories<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
private readonly IImageManagementService imageManagementService;
        public ProductRepository(ApplicationDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(CreateProductDto productDto)
        {
            if (productDto == null)
            { 
                throw new ArgumentNullException(nameof(productDto), "Product DTO cannot be null");
            }
            var product =mapper.Map<Product>(productDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            var imagePath = await imageManagementService.AddImageAsync(productDto.Image, productDto.Name);

            var image = imagePath.Select(path => new ProductImage
            {
                ImageName=path,
                ProductId=product.Id
            }).ToList();
            await context.ProductImages.AddRangeAsync(image);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(UpdateProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Product DTO cannot be null");
            }
            var findedProduct =await context.Products.Include(m=>m.Category).Include(m=>m.ProductImages).FirstOrDefaultAsync(m => m.Id == productDto.Id);
            if (findedProduct == null)
            {
                throw new KeyNotFoundException($"Product with id {productDto.Id} not found.");
            }
            mapper.Map(productDto, findedProduct);
            var findedProductImages = context.ProductImages.Where(m => m.ProductId == productDto.Id).ToList();
            if (findedProductImages != null && findedProductImages.Any())
            {
                foreach (var image in findedProductImages)
                {
                    imageManagementService.DeletImageAsync(image.ImageName);
                }
                context.ProductImages.RemoveRange(findedProductImages);

                var imagePath =await imageManagementService.AddImageAsync(productDto.Image, productDto.Name);
                var newImages = imagePath.Select(path => new ProductImage
                {
                    ImageName = path,
                    ProductId = productDto.Id
                }).ToList();
                await context.ProductImages.AddRangeAsync(newImages);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new KeyNotFoundException($"No images found for product with id {productDto.Id}.");
            }
        }
    }
}
