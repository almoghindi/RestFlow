using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest("Category data is null.");
            }

            await _categoryService.Add(category.Name, category.Description);
            return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryId }, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryService.GetAll();
            if (existingCategory.All(c => c.CategoryId != id))
            {
                return NotFound("Category not found.");
            }

            await _categoryService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _categoryService.GetAll();
            if (existingCategory.All(c => c.CategoryId != id))
            {
                return NotFound("Category not found.");
            }

            await _categoryService.Update(category);
            return NoContent();
        }

    }
}
