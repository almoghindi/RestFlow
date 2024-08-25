using RestFlow.DAL.Entities;

namespace RestFlow.BL.Factory
{
    public class ModelFactory : IModelFactory
    {
        public ModelFactory() { }

        public Category CreateCategory(string name, string description, int restaurantId)
        {
            return new Category
            {
                Name = name,
                Description = description,
                RestaurantId = restaurantId
            };
        }

        public Dish CreateDish(string name, decimal price, int categoryId, string description, int restaurantId)
        {
            return new Dish
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                Description = description,
                IsAvailable = true,
                Ingredients = new List<Ingredient>(),
                RestaurantId = restaurantId
            };
        }

        public Ingredient CreateIngredient(string name, decimal quantity, decimal pricePerUnit, string description, int restaurantId)
        {
            return new Ingredient
            {
                Name = name,
                Quantity = quantity,
                PricePerUnit = pricePerUnit,
                Description = description,
                RestaurantId = restaurantId,
                IsAvailable = quantity > 0,
            };
        }

        public Order CreateOrder(int tableId, int waiterId, int restaurantId)
        {
            return new Order
            {
                TableId = tableId,
                WaiterId = waiterId,
                RestaurantId = restaurantId,
                Dishes = new List<Dish>(),
                OrderDate = DateTime.UtcNow,
                IsCompleted = false,
                TotalAmount = 0,
            };
        }

        public Restaurant CreateRestaurant(string name, string password)
        {
            return new Restaurant
            {
                Name = name,
                Password = password,
            };
        }

        public Table CreateTable(string tableNumber, int capacity, int restaurantId, bool isAvailable = true)
        {
            return new Table
            {
                TableNumber = tableNumber,
                Capacity = capacity,
                RestaurantId = restaurantId,
                IsAvailable = isAvailable,
            };
        }

        public Waiter CreateWaiter(string fullName, string password, string contactInformation, int restaurantId)
        {
            return new Waiter
            {
                FullName = fullName,
                Password = password,
                RestaurantId = restaurantId,
                ContactInformation = contactInformation
            };
        }
    }
}
