using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.Common.DataStructures;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;

namespace RestFlow.BL.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<TableService> _logger;
        private readonly CustomGraph<Table> _tablesGraph;

        public TableService(ITableRepository tableRepository, IModelFactory modelFactory, ILogger<TableService> logger)
        {
            _tableRepository = tableRepository;
            _modelFactory = modelFactory;
            _logger = logger;
            _tablesGraph = new CustomGraph<Table>(table => table.TableId);
        }

        public async Task LoadTablesIntoGraph(int restaurantId)
        {
            try
            {
                _logger.LogInformation($"Loading tables for restaurant ID: {restaurantId} into the graph");
                var tables = await _tableRepository.GetAllByRestaurantId(restaurantId);
                foreach (var table in tables)
                {
                    _tablesGraph.AddNode(table);
                }
                _logger.LogInformation($"Successfully loaded {tables.Count()} tables into the graph");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while loading tables into the graph for restaurant ID: {restaurantId}");
                throw;
            }
        }

        public async Task<Table> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching Table with ID: {id} from the graph");
                var table = _tablesGraph.GetNodeById(id);
                if (table == null)
                {
                    _logger.LogWarning($"Table with ID: {id} not found in the graph, querying the repository");
                    table = await _tableRepository.GetById(id);
                    if (table != null)
                    {
                        _tablesGraph.AddNode(table);
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching Table with ID: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Table>> GetAllByRestaurantId(int restaurantId)
        {
            try
            {
                _logger.LogInformation("Fetching all Tables from the graph");
                var tablesInGraph = _tablesGraph.GetAllNodes();
                if (tablesInGraph == null || !tablesInGraph.Any())
                {
                    _logger.LogInformation("No tables found in the graph, loading from the repository");
                    await LoadTablesIntoGraph(restaurantId);
                    return _tablesGraph.GetAllNodes();
                }
                return tablesInGraph;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all Tables");
                throw;
            }
        }

        public async Task Add(string tableNumber, int capacity, int restaurantId)
        {
            try
            {
                _logger.LogInformation($"Adding Table: {tableNumber}");
                var table = _modelFactory.CreateTable(tableNumber, capacity, restaurantId);

                await _tableRepository.Add(table);
                _tablesGraph.AddNode(table);

                _logger.LogInformation($"Successfully added Table with ID: {table.TableId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding Table: {tableNumber}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting Table with ID: {id}");

                await _tableRepository.Delete(id);
                _tablesGraph.RemoveNode(id);

                _logger.LogInformation($"Successfully deleted Table with ID: {id} from the repository and graph");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Table with ID: {id}");
                throw;
            }
        }
    }
}
