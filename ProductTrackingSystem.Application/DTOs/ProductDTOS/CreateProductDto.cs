
using System.ComponentModel.DataAnnotations;

namespace ProductTrackingSystem.Application.DTOs.ProductDTOS
{
    public class CreateProductDto
    {
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;

        [Range(.01, double.MaxValue, ErrorMessage = "Price must be >= .01")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be >= 1")]
        public int QuantityInStock { get; set; }
    }
}
