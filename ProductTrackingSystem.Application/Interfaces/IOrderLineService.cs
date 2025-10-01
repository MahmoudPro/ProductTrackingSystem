
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;

namespace ProductTrackingSystem.Application.Interfaces
{
    public interface IOrderLineService
    {
        Task<IEnumerable<OrderLineDto>> GetAllOrderLinesAsync();
        Task<OrderLineDto> GetOrderLineByIdAsync(int id);
        Task<IEnumerable<OrderLineDto>> GetOrderLinesByOrderIdAsync(int orderId);
        Task<OrderLineDto> AddOrderLineAsync(CreateOrderLineDto createOrderLineDto);
        Task<bool> UpdateOrderLineAsync(int id, UpdateOrderLineDto updateOrderLineDto);
        Task<bool> DeleteOrderLineAsync(int id);
    }
}
