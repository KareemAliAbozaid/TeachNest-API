using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechNest.API.Helper;
using TechNest.Domain.DTOs.CategoriesDto;
using TechNest.Domain.Entites.Product;
using TechNest.Domain.Interfaces;

namespace TechNest.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await unitOfWork.CategoryRepository.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500));

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new ResponseAPI(404));
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var category = mapper.Map<Category>(categoryDto);
                await unitOfWork.CategoryRepository.AddAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, new ResponseAPI(200, "Category created successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500));
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new ResponseAPI(404));
                }
                mapper.Map(categoryDto, category);
                await unitOfWork.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Category Updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new ResponseAPI(404));
                }

                category.IsDeleted = true;
                category.UpdatedAt = DateTime.Now;
                await unitOfWork.CategoryRepository.UpdateAsync(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseAPI(500));
            }
        }
    }           
}
