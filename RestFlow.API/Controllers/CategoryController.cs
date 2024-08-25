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
        public async Task<IActionResult> GetCategories(int restaurantId)
        {
            if (restaurantId == 0)
            {
                return BadRequest("Restaurant id is 0");
            }

            var categories = await _categoryService.GetAllByRestaurantId(restaurantId);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto category)
        {
            if (category == null)
            {
                return BadRequest("Category data is null.");
            }

            await _categoryService.Add(category.Name, category.Description, category.RestaurantId);
            return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryId }, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == 0)
            {
                return BadRequest("Category id is 0");
            }
            await _categoryService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if(id == 0)
            {
                return BadRequest("Category id is 0");
            }
            if (category == null)
            {
                return BadRequest("Category data is null.");
            }
            if (id != category.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            await _categoryService.Update(category);
            return NoContent();
        }

    }
}
