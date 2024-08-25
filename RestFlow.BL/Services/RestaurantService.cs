using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using RestFlow.Common.Utilities;

namespace RestFlow.BL.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository restaurantRepository, IModelFactory modelFactory, ILogger<RestaurantService> logger)
        {
            _restaurantRepository = restaurantRepository;
            _modelFactory = modelFactory;
            _logger = logger;
        }

        public async Task<Restaurant> GetById(int id)
        {
            try
            {
                var restaurant = await _restaurantRepository.GetById(id);
                _logger.LogInformation($"Fetched restaurant with ID: {id}");
                return restaurant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching restaurant with ID: {id}");
                throw;
            }
        }

        public async Task<Restaurant> Create(string name, string password)
        {
            try
            {
                var encryptedPassword = EncryptionUtility.Encrypt(password);
                var restaurant = _modelFactory.CreateRestaurant(name, encryptedPassword);
                var createdRestaurant = await _restaurantRepository.Create(restaurant);
                _logger.LogInformation($"Created restaurant: {createdRestaurant}");
                return createdRestaurant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating restaurant with Name: {name}");
                throw;
            }
        }

        public async Task<Restaurant> Login(string name, string password)
        {
            try
            {
                var decryptedPassword = EncryptionUtility.Decrypt(password);
                var restaurant = await _restaurantRepository.Login(name, decryptedPassword);
                if (restaurant == null)
                {
                    _logger.LogWarning($"Login failed for restaurant: {name}");
                    return null;
                }
                return restaurant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during login for restaurant: {name}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Restaurant Table with ID: {id}");
                await _restaurantRepository.Delete(id);
                _logger.LogInformation($"Successfully deleted Restaurant with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Restaurant with ID: {id}");
                throw;
            }
        }
    }
}
