using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllByRestaurantId(int restaurantId);
        Task<Dish> GetById(int dishId);
        Task Add(Dish dish);
        Task Update(Dish dish);
        Task Delete(int dishId);
    }
}
