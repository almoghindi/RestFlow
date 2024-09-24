using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface IDishService
    {
        Task<IEnumerable<Dish>> GetAllByRestaurantId(int restaurantId);
        Task<Dish> GetById(int dishId);
        Task Add(string name, decimal price, int categoryId, bool isAvailable, List<int> ingredients, string description, int restaurantId);
        Task Update(int id, string name, decimal price, int categoryId, bool isAvailable, List<int> ingredients, string imageUrl, int restaurantId);
        Task Delete(int dishId);
        IEnumerable<Dish> GetDishesByCategory(int categoryId);

    }
}
