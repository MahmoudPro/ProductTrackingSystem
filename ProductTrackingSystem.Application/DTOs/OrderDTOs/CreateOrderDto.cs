using System.ComponentModel.DataAnnotations;
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;

namespace ProductTrackingSystem.Application.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        // reqular just a-z A-Z
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string CustomerName { get; set; }

        public string? Notes { get; set; }

        public List<CreateOrderLineDto> OrderLines { get; set; } = new();

    }
}
