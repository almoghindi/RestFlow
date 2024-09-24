using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.Common.DataStructures;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;

namespace RestFlow.BL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<IngredientService> _logger;
        private readonly CustomTree<Ingredient> _ingredientTree;

        public IngredientService(IIngredientRepository ingredientRepository, IModelFactory modelFactory, ILogger<IngredientService> logger)
        {
            _ingredientRepository = ingredientRepository;
            _modelFactory = modelFactory;
            _logger = logger;
            _ingredientTree = new CustomTree<Ingredient>(null);
        }

        public async Task<Ingredient> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve ingredient with ID {id}");
                var ingredient = await _ingredientRepository.GetById(id);
                if (ingredient == null)
                {
                    _logger.LogWarning($"Ingredient with ID {id} not found");
                }
                return ingredient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving ingredient with ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Ingredient>> GetAllByRestaurantId(int restaurantId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all ingredients");
                var ingredients = await _ingredientRepository.GetAllByRestaurantId(restaurantId);

                PopulateTree(ingredients);
                return ingredients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all ingredients");
                throw;
            }
        }

        private void PopulateTree(IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                _ingredientTree.AddChild(_ingredientTree.Root, ingredient);
            }
        }

        public async Task Add(string name, decimal quantity, decimal pricePerUnit, string description, int restaurantId)
        {
            try
            {
                _logger.LogInformation($"Attempting to add a new ingredient: {name}");
                Ingredient ingredient = _modelFactory.CreateIngredient(name, quantity, pricePerUnit, description, restaurantId);
                await _ingredientRepository.Add(ingredient);

                _ingredientTree.AddChild(_ingredientTree.Root, ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a new ingredient: {name}");
                throw;
            }
        }

        public async Task Update(int id, string name, decimal quantity, decimal pricePerUnit, string description, int restaurantId)
        {
            try
            {
                _logger.LogInformation($"Attempting to update ingredient with ID {id}");
                if (id == 0 || name == null || quantity < 0 || pricePerUnit < 0 || description == null || restaurantId == 0)
                {
                    _logger.LogWarning("Ingredient data is null.");
                    throw new ArgumentNullException(name, "Ingredient cannot be null.");
                }

                var existingIngredient = await _ingredientRepository.GetById(id);
                if (existingIngredient == null)
                {
                    _logger.LogWarning("Ingredient with ID: {id} not found for update.", id);
                    throw new KeyNotFoundException("Ingredient not found.");
                }

                Ingredient ingredient = new() { IngredientId = id, Name = name, Quantity = quantity, PricePerUnit = pricePerUnit, Description = description, IsAvailable = quantity > 0, RestaurantId = restaurantId };
                await _ingredientRepository.Update(ingredient);

                _ingredientTree.RemoveChild(_ingredientTree.Root, existingIngredient);
                _ingredientTree.AddChild(_ingredientTree.Root, ingredient);
                _logger.LogInformation("Ingredient updated successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating ingredient with ID {id}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete ingredient with ID {id}");
                var ingredient = await _ingredientRepository.GetById(id);
                await _ingredientRepository.Delete(id);

                if (ingredient != null)
                {
                    _ingredientTree.RemoveChild(_ingredientTree.Root, ingredient);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting ingredient with ID {id}");
                throw;
            }
        }

        public async Task AddQuantity(int id, decimal quantityToAdd)
        {
            try
            {
                _logger.LogInformation($"Attempting to add quantity {quantityToAdd} to ingredient with ID {id}");
                await _ingredientRepository.AddQuantity(id, quantityToAdd);
                var ingredient = await _ingredientRepository.GetById(id);
                if (ingredient != null)
                {
                    _ingredientTree.RemoveChild(_ingredientTree.Root, ingredient);
                    ingredient.Quantity += quantityToAdd;
                    _ingredientTree.AddChild(_ingredientTree.Root, ingredient);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding quantity to ingredient with ID {id}");
                throw;
            }
        }

        public async Task RemoveQuantity(int id, decimal quantityToRemove)
        {
            try
            {
                _logger.LogInformation($"Attempting to remove quantity {quantityToRemove} from ingredient with ID {id}");
                await _ingredientRepository.RemoveQuantity(id, quantityToRemove);
                var ingredient = await _ingredientRepository.GetById(id);
                if (ingredient != null)
                {
                    _ingredientTree.RemoveChild(_ingredientTree.Root, ingredient);
                    ingredient.Quantity -= quantityToRemove;
                    _ingredientTree.AddChild(_ingredientTree.Root, ingredient);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing quantity from ingredient with ID {id}");
                throw;
            }
        }
    }
}
