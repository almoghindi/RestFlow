using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestFlow.BL.Factory;

namespace RestFlow.BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IModelFactory modelFactory, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _modelFactory = modelFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllByRestaurantId(int restaurantId)
        {
            try
            {
                _logger.LogInformation("Fetching all categories from BL.");
                var categories = await _categoryRepository.GetAllByRestaurantId(restaurantId);
                _logger.LogInformation("Categories fetched successfully from BL.");
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all categories in BL.");
                throw; 
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching all categories from BL.");
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                {
                    _logger.LogError("An error occurred while adding a category in BL.");
                    throw new Exception();
                }
                _logger.LogInformation("Categories fetched successfully from BL.");
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a category in BL.");
                throw;
            }
        }

        public async Task Add(string name, string description, int restaurantId)
        {
            try
            {
                _logger.LogInformation("Adding a new category from BL.");
                Category category = _modelFactory.CreateCategory(name, description, restaurantId);
                await _categoryRepository.Add(category);
                _logger.LogInformation("Category added successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a category in BL.");
                throw; 
            }
        }

        public async Task Delete(int categoryId)
        {
            try
            {
                _logger.LogInformation("Deleting category from BL. Category ID: {CategoryId}", categoryId);
                await _categoryRepository.Delete(categoryId);
                _logger.LogInformation("Category deleted successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a category in BL. Category ID: {CategoryId}", categoryId);
                throw; 
            }
        }

        public async Task Update(int id, string name, string description, int restaurantId)
        {
            try
            {
                _logger.LogInformation("Updating category from BL. Category ID: {CategoryId}", id);
                await _categoryRepository.Update(id, name, description, restaurantId);
                _logger.LogInformation("Category updated successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a category in BL. Category ID: {CategoryId}", id);
                throw; 
            }
        }
    }
}
