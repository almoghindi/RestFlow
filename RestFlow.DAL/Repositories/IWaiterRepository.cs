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
        Task<IEnumerable<Waiter>> GetAll();
        Task Add(Waiter waiter);
        Task Delete(int id);
    }
}
