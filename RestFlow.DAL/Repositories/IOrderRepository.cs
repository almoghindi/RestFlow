using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> InitializeOrder(Order order);
        Task<Order> GetOrderById(int orderId);
        Task AddDishToOrder(int orderId, Dish dish);
        Task RemoveDishFromOrder(int orderId, int dishId);
        Task<IEnumerable<Order>> GetIncompleteOrders();

        Task<Order> CloseAndPayOrder(int orderId);
        Task<IEnumerable<Order>> GetOrdersQueue();
    }
}
