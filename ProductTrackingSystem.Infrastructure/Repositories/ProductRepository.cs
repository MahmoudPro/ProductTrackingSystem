using Microsoft.EntityFrameworkCore;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;
using ProductTrackingSystem.Infrastructure.Contexts;


namespace ProductTrackingSystem.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PTSContext _context;
        public ProductRepository(PTSContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _= _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                return false;
            _context.Products.Update(product);
            return await SaveChangesAsync() > 0;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<Product> GetBySkuAsync(string sku)
        {
           return _context.Products.FirstOrDefaultAsync(p => p.SKU == sku)!;
        }
    }
}
