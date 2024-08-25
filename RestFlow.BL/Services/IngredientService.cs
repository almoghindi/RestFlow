using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<IngredientService> _logger;

        public IngredientService(IIngredientRepository ingredientRepository, IModelFactory modelFactory, ILogger<IngredientService> logger)
        {
            _ingredientRepository = ingredientRepository;
            _modelFactory = modelFactory;
            _logger = logger;
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
                return await _ingredientRepository.GetAllByRestaurantId(restaurantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all ingredients");
                throw;
            }
        }

        public async Task Add(string name, decimal quantity, decimal pricePerUnit, string description, int restaurantId)
        {
            try
            {
                _logger.LogInformation($"Attempting to add a new ingredient: {name}");
                Ingredient ingredient = _modelFactory.CreateIngredient(name, quantity, pricePerUnit, description, restaurantId);
                await _ingredientRepository.Add(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a new ingredient: {name}");
                throw;
            }
        }

        public async Task Update(Ingredient ingredient)
        {
            try
            {
                _logger.LogInformation($"Attempting to update ingredient with ID {ingredient.IngredientId}");
                await _ingredientRepository.Update(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating ingredient with ID {ingredient.IngredientId}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete ingredient with ID {id}");
                await _ingredientRepository.Delete(id);
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while removing quantity from ingredient with ID {id}");
                throw;
            }
        }
    }
}
