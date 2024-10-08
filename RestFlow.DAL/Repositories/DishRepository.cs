﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly DataDbContext _context;
        private readonly ILogger<DishRepository> _logger;

        public DishRepository(DataDbContext context, ILogger<DishRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Dish>> GetAllByRestaurantId(int restaurantId)
        {
            _logger.LogInformation("Fetching all dishes from DAL.");
            var dishes = await _context.Dishes.Where(d => d.RestaurantId == restaurantId).Include(d => d.Ingredients).ToListAsync();
            //foreach (var dish in dishes)
            //{
            //    dish.AreAllIngredientsAvailable();
            //}
            return dishes;
        }

        public async Task<Dish> GetById(int dishId)
        {
            _logger.LogInformation("Fetching dish with ID: {DishId} from DAL.", dishId);
            return await _context.Dishes.FindAsync(dishId);
        }

        public async Task Add(Dish dish)
        {
            _logger.LogInformation("Adding dish with ID: {DishId} in DAL.", dish.DishId);
            _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Dish added successfully in DAL.");

        }

        public async Task Update(Dish dish)
        {
            _logger.LogInformation("Updating dish with ID: {DishId} in DAL.", dish.DishId);
            var dishToUpdate = await _context.Dishes.SingleAsync(d => d.DishId == dish.DishId && d.RestaurantId == dish.RestaurantId);

            dishToUpdate.Name = dish.Name;
            dishToUpdate.Price = dish.Price;
            dishToUpdate.CategoryId = dish.CategoryId;
            dishToUpdate.IsAvailable = dish.IsAvailable;
            dishToUpdate.ImageUrl = dish.ImageUrl;

            _context.SaveChanges();
            _logger.LogInformation("Dish updated successfully in DAL.");
        }

        public async Task Delete(int dishId)
        {
            _logger.LogInformation("Deleting dish with ID: {DishId} from DAL.", dishId);
            var dish = await _context.Dishes.FindAsync(dishId);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dish deleted successfully from DAL.");
            }
            else
            {
                _logger.LogWarning("Dish with ID: {DishId} not found for deletion.", dishId);
            }
        }
    }
}
