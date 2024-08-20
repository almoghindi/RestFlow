using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface IIngredientService
    {
        Task<Ingredient> GetById(int id);
        Task<IEnumerable<Ingredient>> GetAll();
        Task Add(string name, decimal quantity, decimal pricePerUnit, string description);
        Task Update(Ingredient ingredient);
        Task Delete(int id);
        Task AddQuantity(int id, decimal quantityToAdd);
        Task RemoveQuantity(int id, decimal quantityToRemove);
    }
}
