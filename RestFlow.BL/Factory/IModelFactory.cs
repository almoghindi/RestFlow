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
        Category CreateCategory(string name, string description, int restaurantId);
        Dish CreateDish(string name, decimal price, int categoryId, string description, int restaurantId);
        Ingredient CreateIngredient(string name, decimal quantity, decimal pricePerUnit, string imageUrl, int restaurantId);
        Order CreateOrder(int tableId, int waiterId, int restaurantId);
        Restaurant CreateRestaurant(string name, string password);
        Table CreateTable(string tableNumber, int capacity, int restaurantIdbool,bool isAvailable = true);
        Waiter CreateWaiter(string fullName, string password, string contactInformation, int restaurantId);
    }
}
