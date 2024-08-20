using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Factory
{
    public interface IModelFactory
    {
        Category CreateCategory(string name, string description);
        Dish CreateDish(string name, decimal price, int categoryId, List<Ingredient> ingredients, string description);
        Ingredient CreateIngredient(string name, decimal quantity, decimal pricePerUnit, string description);
        Order CreateOrder(int tableId, int waiterId);
        Table CreateTable(string tableNumber, int capacity, bool isAvailable = true);
        Waiter CreateWaiter(string fullName, string password, string contactInformation);
    }
}
