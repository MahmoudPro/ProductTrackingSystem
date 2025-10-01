
namespace ProductTrackingSystem.Application.DTOs.ProductDTOS
{
    public class UpdateProductDto
    {
        public string Name { get; set; } 
        public string SKU { get; set; } 
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
