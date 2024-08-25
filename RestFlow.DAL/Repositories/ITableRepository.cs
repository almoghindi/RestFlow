using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public interface ITableRepository
    {
        Task<Table> GetById(int id);
        Task<IEnumerable<Table>> GetAllByRestaurantId(int restaurantId);
        Task Add(Table table);
        Task Delete(int id);
    }
}
