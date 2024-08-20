using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDishes()
        {
            var dishes = await _dishService.GetAll();
            return Ok(dishes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            var dish = await _dishService.GetById(id);
            if (dish == null)
            {
                return NotFound("Dish not found.");
            }
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> AddDish(DishDto dish)
        {
            if (dish == null)
            {
                return BadRequest("Dish data is null.");
            }
            Console.WriteLine(dish);

            await _dishService.Add(dish.Name, dish.Price,dish.CategoryId ,dish.IsAvailable, dish.IngredientsId, dish.Description);
            return Ok(nameof(GetDishById));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDish(int id, Dish dish)
        {
            if (id != dish.DishId)
            {
                return BadRequest("Dish ID mismatch.");
            }

            var existingDish = await _dishService.GetById(id);
            if (existingDish == null)
            {
                return NotFound("Dish not found.");
            }

            await _dishService.Update(dish);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var existingDish = await _dishService.GetById(id);
            if (existingDish == null)
            {
                return NotFound("Dish not found.");
            }

            await _dishService.Delete(id);
            return NoContent();
        }
    }
}
