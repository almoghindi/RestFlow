using RestFlow.DAL.Entities;

namespace RestFlow.BL.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllByRestaurantId(int restaurantId);
        Task<Category> GetById(int id);
        Task Add(string name, string description, int restaurantId);
        Task Delete(int categoryId);
        Task Update(int id, string name, string description, int restaurantId);
    }
}
