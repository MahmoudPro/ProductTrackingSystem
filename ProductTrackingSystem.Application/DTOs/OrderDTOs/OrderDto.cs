using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;

namespace ProductTrackingSystem.Application.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
    }
}
