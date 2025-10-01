
using System.ComponentModel.DataAnnotations;

namespace ProductTrackingSystem.Application.DTOs.ProductDTOS
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be >= 0")]
        public int QuantityInStock { get; set; }
    }
}
