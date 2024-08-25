using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> InitializeOrder(Order order);
        Task<Order> GetOrderById(int orderId);
        Task AddDishToOrder(int orderId, Dish dish);
        Task RemoveDishFromOrder(int orderId, int dishId);
        Task<IEnumerable<Order>> GetIncompleteOrdersByRestaurantId(int restaurantId);
        Task<IEnumerable<Order>> GetCompletedOrdersByRestaurantId(int restaurantId);

        Task<Order> CloseAndPayOrder(int orderId);
        Task<IEnumerable<Order>> GetOrdersQueue();
    }
}
