using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface IWaiterService
    {
        Task<Waiter> GetById(int id);
        Task<IEnumerable<Waiter>> GetAllByRestaurantId(int restaurantId);
        Task Add(string fullname, string password, string contactInformation, int restaurantId);
        Task Delete(int id);
        Task<Waiter> Login(string fullName, string password, int restaurantId);
    }
}
