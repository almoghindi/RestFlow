using Microsoft.EntityFrameworkCore;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataDbContext _context;

        public RestaurantRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task<Restaurant> Create(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
            return restaurant;
        }
        public async Task<Restaurant> Login(string name, string password)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Name == name && r.Password == password);
        }

        public async Task Delete(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();

            }
        }
    }
}
