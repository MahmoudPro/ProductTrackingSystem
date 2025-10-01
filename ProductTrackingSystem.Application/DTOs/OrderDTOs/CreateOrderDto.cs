using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;

namespace ProductTrackingSystem.Application.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; }

        public string? Notes { get; set; }

        public List<CreateOrderLineDto> OrderLines { get; set; } = new();

    }
}
