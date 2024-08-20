using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<TableService> _logger;

        public TableService(ITableRepository tableRepository ,IModelFactory modelFactory ,ILogger<TableService> logger)
        {
            _tableRepository = tableRepository;
            _modelFactory = modelFactory;
            _logger = logger;
        }

        public async Task<Table> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching Table with ID: {id}");
                var table = await _tableRepository.GetById(id);
                if (table == null)
                {
                    _logger.LogWarning($"Table with ID: {id} not found");
                }
                return table;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching Table with ID: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Table>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all Tables");
                var tables = await _tableRepository.GetAll();
                return tables;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all Tables");
                throw;
            }
        }

        public async Task Add(string tableNumber, int capacity)
        {
            try
            {
                _logger.LogInformation($"Adding Table : {tableNumber}");
                var table = _modelFactory.CreateTable(tableNumber, capacity);
                await _tableRepository.Add(table);
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
                _logger.LogInformation($"Successfully deleted Table with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Table with ID: {id}");
                throw;
            }
        }
    }
}
