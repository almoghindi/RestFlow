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
            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetAll()
        {
            var ingredients = await _ingredientService.GetAll();
            return Ok(ingredients);
        }

        [HttpPost]
        public async Task<ActionResult> Create(IngredientDTO ingredient)
        {
            await _ingredientService.Add(ingredient.Name, ingredient.Quantity, ingredient.PricePerUnit, ingredient.Description);
            return Ok(ingredient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            await _ingredientService.Update(ingredient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ingredientService.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/addQuantity")]
        public async Task<ActionResult> AddQuantity(int id, [FromBody] decimal quantityToAdd)
        {
            await _ingredientService.AddQuantity(id, quantityToAdd);
            return NoContent();
        }

        [HttpPost("{id}/removeQuantity")]
        public async Task<ActionResult> RemoveQuantity(int id, [FromBody] decimal quantityToRemove)
        {
            await _ingredientService.RemoveQuantity(id, quantityToRemove);
            return NoContent();
        }
    }
}
