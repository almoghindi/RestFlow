using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public interface IOrderService
    {
        Task<Order> InitializeOrder(int waiterId, int tableId, int restaurantId);
        Task<Order> GetOrderById(int orderId);
        Task AddDishToOrder(int orderId, int dishId);
        Task RemoveDishFromOrder(int orderId, int dishId);
        Task<IEnumerable<Order>> GetCompletedOrdersByRestaurantId(int restaurantId);
        Task<Order> CloseAndPayOrder(int orderId);
        Task<IEnumerable<Order>> GetOrdersQueue(int restaurantId);
    }
}
