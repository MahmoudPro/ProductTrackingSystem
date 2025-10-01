using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Application.DTOs.OrderDTOs
{
    public class UpdateOrderDto
    {
        public string CustomerName { get; set; } 
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
    }
}
