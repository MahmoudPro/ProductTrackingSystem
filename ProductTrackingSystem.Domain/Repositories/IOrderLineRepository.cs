using ProductTrackingSystem.Domain.Entities;

public interface IOrderLineRepository
{
    Task<IEnumerable<OrderLine>> GetAllAsync();
    Task<IEnumerable<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId);
    Task<OrderLine> GetByIdAsync(int id);
    Task<OrderLine> AddAsync(OrderLine orderLine);
    Task<bool> Update(OrderLine orderLine);
    Task<bool> Delete(int id);
    Task<int> SaveChangesAsync();
}
