using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs
{
    public class CreateProductTrackingLogDto
    {
        public int productId {  get; set; }

        public string ActionType { get; set; }= string.Empty;

        [Range(-1000000, 1000000, ErrorMessage = "Quantity change must be reasonable.")]
        public int QuantityChange { get; set; }

        public string? Description { get; set; }
    }
}
