using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

namespace RestFlow.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWaiterService _waiterService;
        private readonly ITableService _tableService;
        private readonly IDishService _dishService;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<OrderService> _logger;
        private readonly Queue<Order> _ordersQueue;

        public OrderService(IOrderRepository orderRepository, IWaiterService waiterService, ITableService tableService, IDishService dishService, IModelFactory modelFactory, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _waiterService = waiterService;
            _tableService = tableService;
            _dishService = dishService;
            _modelFactory = modelFactory;
            _logger = logger;
            _ordersQueue = new Queue<Order>();
        }

        public async Task<Order> InitializeOrder(int waiterId, int tableId)
        {
            try
            {
                var waiter = await _waiterService.GetById(waiterId);
                var table = await _tableService.GetById(tableId);

                if (waiter == null || table == null)
                {
                    throw new ArgumentException("Invalid Waiter or Table");
                }

                var order = _modelFactory.CreateOrder(tableId, waiterId);
                if(order == null)
                {
                    throw new ArgumentException("Invalid order");
                }
                order.Table = table;
                order.Waiter = waiter;

                _ordersQueue.Enqueue(order);
                _logger.LogInformation($"Order {order.OrderId} added to the queue.");

                return await _orderRepository.InitializeOrder(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing order");
                throw;
            }
        }

        private async Task LoadQueue()
        {
            try
            {
                var incompleteOrders = await _orderRepository.GetIncompleteOrders();

                foreach (var order in incompleteOrders)
                {
                    _ordersQueue.Enqueue(order);
                    _logger.LogInformation($"Order {order.OrderId} loaded into the queue.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading orders into the queue.");
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            try
            {
                return await _orderRepository.GetOrderById(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving order {orderId}");
                throw;
            }
        }

        public async Task AddDishToOrder(int orderId, int dishId)
        {
            try
            {
                var dish = await _dishService.GetById(dishId);
                if (dish == null)
                {
                    throw new ArgumentException("Invalid Dish");
                }

                await _orderRepository.AddDishToOrder(orderId, dish);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding dish to order");
                throw;
            }
        }

        public async Task RemoveDishFromOrder(int orderId, int dishId)
        {
            try
            {
                await _orderRepository.RemoveDishFromOrder(orderId, dishId);
                _logger.LogInformation($"Dish {dishId} removed from order {orderId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing dish {dishId} from order {orderId}");
                throw;
            }
        }

        public async Task<Order> CloseAndPayOrder(int orderId)
        {
            try
            {
                return await _orderRepository.CloseAndPayOrder(orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing and paying order");
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersQueue()
        {
            try
            {
                if(_ordersQueue.Count == 0)
                {
                    await LoadQueue();
                }
                return await _orderRepository.GetOrdersQueue();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders queue");
                throw;
            }
        }
    }
}
