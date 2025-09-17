
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechNest.API.Helper;
using TechNest.Domain.DTOs.ProductDto;
using TechNest.Domain.Interfaces;
using TechNest.Domain.Sharing;

namespace TechNest.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductParams productParams)
        {
            try
            {
                var products = await unitOfWork.ProductRepository.GetAllAsync(productParams);
                var totalRecords = await unitOfWork.ProductRepository.CountAsync();


                return Ok(new Pagination<GetProductDto>(
                    productParams.PageNumber ?? 1, 
                    productParams.pageSize,
                    totalRecords,
                    products
                ));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseApi(500, "An error occurred while fetching products."));
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
                    return BadRequest(new ResponseApi(400, "An error occurred while fetching products."));
                }
                return Ok(resulte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseApi(500, "An error occurred while fetching products."));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto productDto)
        {
            try
            {
                await unitOfWork.ProductRepository.AddAsync(productDto);
                return Ok(new ResponseApi(200));
                    

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseApi(500, "An error occurred while creating the product."));
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto productDto)
        {
            try
            {
                await unitOfWork.ProductRepository.UpdateAsync(productDto);
                return Ok(new ResponseApi(200));
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ResponseApi(404, knfEx.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseApi(500, "An error occurred while updating the product."));
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
                    return NotFound(new ResponseApi(404, "Product not found."));
                }

                product.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();

                return Ok(new ResponseApi(200, "Product deleted successfully."));
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new ResponseApi(404, knfEx.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseApi(500, "An error occurred while deleting the product."));
            }
        }

    }
}
