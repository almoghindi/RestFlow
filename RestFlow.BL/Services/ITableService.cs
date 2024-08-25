using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface ITableService
    {
        Task<Table> GetById(int id);
        Task<IEnumerable<Table>> GetAllByRestaurantId(int restaurantId);
        Task Add(string tableNumber, int capacity, int restaurantId);
        Task Delete(int id);
    }
}
