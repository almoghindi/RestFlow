using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;

namespace RestFlow.BL.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly ILogger<DishService> _logger;
        private readonly IModelFactory _modelFactory;
        private readonly ICategoryService _categoryService;

        private readonly Dictionary<int, List<Dish>> _categoryDishIndex = new Dictionary<int, List<Dish>>();

        public DishService(IDishRepository dishRepository, ILogger<DishService> logger, IModelFactory modelFactory, ICategoryService categoryService)
        {
            _dishRepository = dishRepository;
            _logger = logger;
            _modelFactory = modelFactory;
            _categoryService = categoryService;
            InitializeIndex().Wait();
        }

        private async Task InitializeIndex()
        {
            try
            {
                _logger.LogInformation("Initializing category-dish index.");
                var dishes = await _dishRepository.GetAll();
                UpdateIndex(dishes);
                _logger.LogInformation("Category-dish index initialized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing category-dish index.");
                throw;
            }
        }

        private void UpdateIndex(IEnumerable<Dish> dishes)
        {
            _categoryDishIndex.Clear();
            foreach (var dish in dishes)
            {
                if (!_categoryDishIndex.ContainsKey(dish.CategoryId))
                {
                    _categoryDishIndex[dish.CategoryId] = new List<Dish>();
                }
                _categoryDishIndex[dish.CategoryId].Add(dish);
            }
        }

        public async Task<IEnumerable<Dish>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all dishes from BL.");
                var dishes = await _dishRepository.GetAll();
                _logger.LogInformation("Successfully fetched all dishes.");
                return dishes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all dishes.");
                throw;
            }
        }

        public async Task<Dish> GetById(int dishId)
        {
            try
            {
                _logger.LogInformation("Fetching dish with ID: {DishId} from BL.", dishId);
                var dish = await _dishRepository.GetById(dishId);
                if (dish == null)
                {
                    _logger.LogWarning("Dish with ID: {DishId} not found.", dishId);
                }
                _logger.LogInformation("Successfully fetched dish with ID: {DishId}.", dishId);
                return dish;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching dish with ID: {DishId}.", dishId);
                throw;
            }
        }

        public async Task Add(string name, decimal price,int categoryId, bool isAvailable, List<int> ingredients, string description)
        {
            try
            {
                _logger.LogInformation("Adding a new dish from BL.");
                Category category =  await _categoryService.GetById(categoryId);
                if (category == null)
                {
                    _logger.LogError("Error adding dish.");
                    throw new Exception();
                }

                Dish dish = _modelFactory.CreateDish(name, price, categoryId, new List<Ingredient> (), description);
                dish.Category = category;
                if (dish == null)
                {
                    _logger.LogWarning("Dish data is null.");
                    throw new ArgumentNullException(nameof(dish), "Dish cannot be null.");
                }

                await _dishRepository.Add(dish);
                UpdateIndex(new[] { dish });
                _logger.LogInformation("Dish added successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding dish.");
                throw;
            }
        }

        public async Task Update(Dish dish)
        {
            try
            {
                _logger.LogInformation("Updating dish with ID: {DishId} in BL.", dish.DishId);
                if (dish == null)
                {
                    _logger.LogWarning("Dish data is null.");
                    throw new ArgumentNullException(nameof(dish), "Dish cannot be null.");
                }
                var existingDish = await _dishRepository.GetById(dish.DishId);
                if (existingDish == null)
                {
                    _logger.LogWarning("Dish with ID: {DishId} not found for update.", dish.DishId);
                    throw new KeyNotFoundException("Dish not found.");
                }
                await _dishRepository.Update(dish);
                UpdateIndex(new[] { dish });
                _logger.LogInformation("Dish updated successfully in BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating dish with ID: {DishId}.", dish.DishId);
                throw;
            }
        }

        public async Task Delete(int dishId)
        {
            try
            {
                _logger.LogInformation("Deleting dish with ID: {DishId} from BL.", dishId);
                var dish = await _dishRepository.GetById(dishId);
                if (dish == null)
                {
                    _logger.LogWarning("Dish with ID: {DishId} not found for deletion.", dishId);
                    throw new KeyNotFoundException("Dish not found.");
                }
                await _dishRepository.Delete(dishId);
                if (_categoryDishIndex.ContainsKey(dish.CategoryId))
                {
                    _categoryDishIndex[dish.CategoryId].Remove(dish);
                    if (_categoryDishIndex[dish.CategoryId].Count == 0)
                    {
                        _categoryDishIndex.Remove(dish.CategoryId);
                    }
                }
                _logger.LogInformation("Dish deleted successfully from BL.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting dish with ID: {DishId}.", dishId);
                throw;
            }
        }

        public IEnumerable<Dish> GetDishesByCategory(int categoryId)
        {
            try
            {
                _logger.LogInformation("Fetching dishes by category ID: {CategoryId}.", categoryId);
                if (_categoryDishIndex.TryGetValue(categoryId, out var dishes))
                {
                    _logger.LogInformation("Successfully fetched dishes for category ID: {CategoryId}.", categoryId);
                    return dishes;
                }
                _logger.LogWarning("No dishes found for category ID: {CategoryId}.", categoryId);
                return Enumerable.Empty<Dish>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching dishes by category ID: {CategoryId}.", categoryId);
                throw;
            }
        }
    }
}
