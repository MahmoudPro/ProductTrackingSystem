

using System.ComponentModel.DataAnnotations;

namespace ProductTrackingSystem.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock cannot be negative.")]
        public int QuantityInStock { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public ICollection<ProductTrackingLog> TrackingLogs { get; set; } = new List<ProductTrackingLog>();

    }
}
