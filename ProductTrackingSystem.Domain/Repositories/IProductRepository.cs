using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByIdAsync(int id);
        //get product by sku
        Task<Product> GetBySkuAsync(string sku);
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
