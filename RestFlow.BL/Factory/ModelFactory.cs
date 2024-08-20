using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Factory
{
    public class ModelFactory : IModelFactory
    {
        public ModelFactory() { }

        public Category CreateCategory(string name, string description)
        {
            return new Category
            {
                Name = name,
                Description = description,
            };
        }

        public Dish CreateDish(string name, decimal price, int categoryId, List<Ingredient> ingredients, string description)
        {
            return new Dish
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                Description = description,
                IsAvailable = true,
                Ingredients = ingredients
            };
        }

        public Ingredient CreateIngredient(string name, decimal quantity, decimal pricePerUnit, string description)
        {
            return new Ingredient
            {
                Name = name,
                Quantity = quantity,
                PricePerUnit = pricePerUnit,
                Description = description,
                IsAvailable = quantity > 0,
            };
        }

        public Order CreateOrder(int tableId, int waiterId)
        {
            return new Order
            {
                TableId = tableId,
                WaiterId = waiterId,
                Dishes = new List<Dish>(),
                OrderDate = DateTime.UtcNow,
                IsCompleted = false,
                TotalAmount = 0,
            };
        }

        public Table CreateTable(string tableNumber, int capacity, bool isAvailable = true)
        {
            return new Table
            {
                TableNumber = tableNumber,
                Capacity = capacity,
                IsAvailable = isAvailable,
            };
        }

        public Waiter CreateWaiter(string fullName, string password, string contactInformation)
        {
            return new Waiter
            {
                FullName = fullName,
                Password = password,
                ContactInformation = contactInformation
            };
        }
    }
}
