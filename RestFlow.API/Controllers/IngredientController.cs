using Microsoft.AspNetCore.Mvc;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;
using RestFlow.API.DTO;

namespace RestFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Ingredient id is 0");
            }

            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetAll(int restaurantId)
        {
            if(restaurantId == 0)
            {
                return BadRequest("Restaurant id is empty");
            }
            var ingredients = await _ingredientService.GetAllByRestaurantId(restaurantId);
            if (ingredients == null)
            {
                return NotFound();
            }
            return Ok(ingredients);
        }

        [HttpPost]
        public async Task<ActionResult> Create(IngredientDTO ingredient)
        {
            if (ingredient == null)
            {
                return BadRequest("Ingredient is null");
            }

            await _ingredientService.Add(ingredient.Name, ingredient.Quantity, ingredient.PricePerUnit, ingredient.Description, ingredient.RestaurantId);
            return Ok(ingredient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, IngredientDTO ingredient)
        {
            if (id == 0)
            {
                return BadRequest("Ingredient id is 0");
            }
            if (ingredient == null)
            {
                return BadRequest("Ingredient is null");
            }

            await _ingredientService.Update(id, ingredient.Name, ingredient.Quantity, ingredient.PricePerUnit, ingredient.Description, ingredient.RestaurantId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Ingredient id is 0");
            }
            await _ingredientService.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/addQuantity")]
        public async Task<ActionResult> AddQuantity(int id, decimal quantityToAdd)
        {
            if (id == 0)
            {
                return BadRequest("Ingredient id is 0");
            }
            if (quantityToAdd == 0)
            {      
                return BadRequest("Quantity To Add is 0");               
            }
            await _ingredientService.AddQuantity(id, quantityToAdd);
            return NoContent();
        }

        [HttpPost("{id}/removeQuantity")]
        public async Task<ActionResult> RemoveQuantity(int id, decimal quantityToRemove)
        {
            if (id == 0)
            {
                return BadRequest("Ingredient id is 0");
            }
            if (quantityToRemove == 0)
            {
                return BadRequest("Quantity To Remove is 0");
            }
            await _ingredientService.RemoveQuantity(id, quantityToRemove);
            return NoContent();
        }
    }
}
