using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllByRestaurantId(int restaurantId);
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Delete(int categoryId);
        Task Update(Category category);
    }
}
