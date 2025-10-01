using ProductTrackingSystem.Infrastructure.Contexts;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ProductTrackingSystem.Infrastructure.Repositories
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly PTSContext _context;

        public OrderLineRepository(PTSContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderLine>> GetAllAsync()
        {
            return await _context.OrderLines.ToListAsync();
        }
        public async Task<IEnumerable<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId)
        {
            return await _context.OrderLines.Where(ol => ol.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderLine> GetByIdAsync(int id)
        {
            return await _context.OrderLines.FindAsync(id);
        }

        public async Task<OrderLine> AddAsync(OrderLine orderLine)
        {
            await _context.OrderLines.AddAsync(orderLine);
            await SaveChangesAsync();
            return orderLine;
        }

        public async Task<bool> Update(OrderLine orderLine)
        {
            _context.OrderLines.Update(orderLine);
             return await SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int  Id)
        {
            var orderline = await _context.OrderLines.FindAsync(Id);
            if (orderline == null)
                return false;

            _context.OrderLines.Remove(orderline);
            return await SaveChangesAsync() > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

 
    }
}
