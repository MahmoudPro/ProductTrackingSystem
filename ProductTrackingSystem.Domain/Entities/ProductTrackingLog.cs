using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTrackingSystem.Domain.Entities
{
    public class ProductTrackingLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public TrackingAction Action { get; set; }

        public int QuantityChange { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    
    }

    public enum TrackingAction
    {
        Added,       
        Updated,      
        AddedStock,   
        RemovedStock,
        Sold,        
        Adjusted    
    }
}
