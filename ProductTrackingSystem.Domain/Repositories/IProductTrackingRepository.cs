

using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Domain.Repositories
{
    public interface IProductTrackingRepository
    {
        Task AddAsync(ProductTrackingLog log);
        Task<IEnumerable<ProductTrackingLog>> GetLogsByProductIdAsync(int productId);

        Task<int> SaveChanges();
    }
}
