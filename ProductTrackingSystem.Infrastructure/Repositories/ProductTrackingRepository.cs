using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;
using ProductTrackingSystem.Infrastructure.Contexts;

namespace ProductTrackingSystem.Infrastructure.Repositories
{
    public class ProductTrackingRepository : IProductTrackingRepository
    {
        private readonly PTSContext _Context;

        public ProductTrackingRepository(PTSContext pTSContext)
        {
            _Context = pTSContext;
        }
        public async Task AddAsync(ProductTrackingLog log)
        {
            await _Context.ProductTrackingLogs.AddAsync(log);
            _ = await SaveChanges();
        }

        public async Task<IEnumerable<ProductTrackingLog>> GetLogsByProductIdAsync(int productId)
        {
            return await _Context.ProductTrackingLogs
                .Where(p_log => p_log.ProductId == productId)
                .OrderByDescending(p_log => p_log.ActionDate)
                .ToListAsync();
        }


        public async Task<int> SaveChanges()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
