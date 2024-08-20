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
        public async Task<IEnumerable<Category>> GetAll()
        {
            _logger.LogInformation($"{nameof(GetAll)}");
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
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



        public async Task Update(Category category)
        {
            var categoryToUpdate = await _context.Categories.SingleAsync(c => c.CategoryId == category.CategoryId);
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            _context.SaveChanges();
            _logger.LogInformation("Category updated succesfully");
        }
    }
}
