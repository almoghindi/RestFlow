using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public interface IWaiterRepository
    {
        Task<Waiter> GetById(int id);
        Task<IEnumerable<Waiter>> GetAllByRestaurantId(int restaurantId);
        Task Add(Waiter waiter);
        Task Delete(int id);
        Task<Waiter> Login(string fullName, string password, int restaurantId);

    }
}
