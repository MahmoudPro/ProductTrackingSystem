using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTrackingSystem.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
    
}
