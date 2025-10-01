using ProductTrackingSystem.Application.DTOs.OrderDTOs;

namespace ProductTrackingSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersWithOrderLinesAsync();
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<(bool Success, string? Message, OrderDto? Order)> PlaceOrderAsync(CreateOrderDto createOrderDto);
        Task<bool> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<(bool Success, string? Message)> CheckStockAsync(CreateOrderDto createOrderDto);
    }
}
