using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Application.DTOs.OrderLineDTOs
{
    public class CreateOrderLineDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be >= 1")]
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
}
