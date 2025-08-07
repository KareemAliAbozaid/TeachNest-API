
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechNest.API.Helper;
using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Interfaces;

namespace TechNest.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await unitOfWork.ProductRepository.GetAllAsync(p => !p.IsDeleted, p => p.Category,p => p.ProductImages);
                var resulte = mapper.Map<List<CreateProductDto>>(products);
                if (products is null)
                {
                    return BadRequest(new ResponseAPI(400, "An error occurred while fetching products."));
                }

                return Ok(resulte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500, "An error occurred while fetching products."));
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(id, p => p.Category, p => p.ProductImages);
                var resulte = mapper.Map<GetProductDto>(product);
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400, "An error occurred while fetching products."));
                }
                return Ok(resulte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500, "An error occurred while fetching products."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto productDto)
        {
            try
            {
                await unitOfWork.ProductRepository.AddAsync(productDto);
                return Ok(new ResponseAPI(200));
                    

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500, "An error occurred while creating the product."));
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto productDto)
        {
            try
            {
                await unitOfWork.ProductRepository.UpdateAsync(productDto);
                return Ok(new ResponseAPI(200));
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ResponseAPI(404, knfEx.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500, "An error occurred while updating the product."));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await unitOfWork.ProductRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new ResponseAPI(404, "Product not found."));
                }

                product.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();

                return Ok(new ResponseAPI(200, "Product deleted successfully."));
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ResponseAPI(404, knfEx.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500, "An error occurred while deleting the product."));
            }
        }

    }
}
