
using System.ComponentModel.DataAnnotations;

namespace ProductTrackingSystem.Application.DTOs.ProductDTOS
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string SKU { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be >= 0")]
        public int QuantityInStock { get; set; }
    }
}
