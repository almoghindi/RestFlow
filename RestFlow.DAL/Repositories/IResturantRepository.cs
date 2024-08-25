using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetById(int id);
        Task<Restaurant> Create(Restaurant restaurant);
        Task<Restaurant> Login(string name, string password);
        Task Delete(int id);
    }
}
