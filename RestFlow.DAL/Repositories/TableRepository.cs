using Microsoft.EntityFrameworkCore;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.DAL.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly DataDbContext _context;
        public TableRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<Table> GetById(int id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<IEnumerable<Table>> GetAllByRestaurantId(int restaurantId)
        {
            return await _context.Tables.Where(t => t.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task Add(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }
    }
}
