using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataDbContext _context;
        private readonly ILogger<IngredientRepository> _logger;


        public IngredientRepository(DataDbContext context, ILogger<IngredientRepository> logger)
        {
            _context = context; 
            _logger = logger;
        }

        public async Task<Ingredient> GetById(int id)
        {
            _logger.LogInformation($"Getting ingredient with ID {id}");
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetAllByRestaurantId(int restaurantId)
        {
            _logger.LogInformation("Getting all ingredients");
            return await _context.Ingredients.Where(i => i.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task Add(Ingredient ingredient)
        {
            _logger.LogInformation($"Adding a new ingredient: {ingredient.Name}");
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ingredient ingredient)
        {
            _logger.LogInformation($"Updating ingredient with ID {ingredient.IngredientId}");
            var ingredientToUpdate = await _context.Ingredients.SingleAsync(i => i.IngredientId == ingredient.IngredientId && i.RestaurantId == ingredient.RestaurantId);

            ingredientToUpdate.Name = ingredient.Name;
            ingredientToUpdate.Quantity = ingredient.Quantity;
            ingredientToUpdate.PricePerUnit = ingredient.PricePerUnit;
            ingredientToUpdate.Description = ingredient.Description;
            ingredientToUpdate.IsAvailable = ingredient.IsAvailable;


            _context.SaveChanges();
            _logger.LogInformation("Ingredient updated successfully in DAL.");

        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleting ingredient with ID {id}");
            var ingredient = await GetById(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddQuantity(int id, decimal quantityToAdd)
        {
            _logger.LogInformation($"Adding quantity {quantityToAdd} to ingredient with ID {id}");
            var ingredient = await GetById(id);
            if (ingredient != null)
            {
                ingredient.AddQuantity(quantityToAdd);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveQuantity(int id, decimal quantityToRemove)
        {
            _logger.LogInformation($"Removing quantity {quantityToRemove} from ingredient with ID {id}");
            var ingredient = await GetById(id);
            if (ingredient != null)
            {
                ingredient.RemoveQuantity(quantityToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
