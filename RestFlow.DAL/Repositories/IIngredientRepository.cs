using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public interface IIngredientRepository
    {
        Task<Ingredient> GetById(int id);
        Task<IEnumerable<Ingredient>> GetAllByRestaurantId(int restaurantId);
        Task Add(Ingredient ingredient);
        Task Update(Ingredient ingredient);
        Task Delete(int id);
        Task AddQuantity(int id, decimal quantityToAdd);
        Task RemoveQuantity(int id, decimal quantityToRemove);
    }
}
