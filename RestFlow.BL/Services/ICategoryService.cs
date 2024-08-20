using RestFlow.DAL.Entities;

namespace RestFlow.BL.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(string name, string description);
        Task Delete(int categoryId);
        Task Update(Category category);
    }
}
