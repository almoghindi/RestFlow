using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;
using System.Net.Http.Headers;

namespace RestFlow.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(DataDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Category>> GetAllByRestaurantId(int restaurantId)
        {
            _logger.LogInformation($"{nameof(GetAllByRestaurantId)}");
            return await _context.Categories.Where(c => c.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            _logger.LogInformation($"{nameof(GetById)}");
            return await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category added succesfully");
        }

        public async Task Delete(int categoryId)
        {
            Category category = await _context.Categories.SingleAsync(c => c.CategoryId == categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Category deleted succesfully");
        }



        public async Task Update(int id, string name, string description, int restaurantId)
        {
            var categoryToUpdate = await _context.Categories.SingleAsync(c => c.CategoryId == id && c.RestaurantId == restaurantId);
            categoryToUpdate.Name = name;
            categoryToUpdate.Description = description;
            _context.SaveChanges();
            _logger.LogInformation("Category updated succesfully");
        }
    }
}
