using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs
{
    public class ProductTrackingLogDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime ActionDate { get; set; }

        public TrackingAction Action { get; set; }

        public int QuantityChange { get; set; }

        public string? Description { get; set; }

    }
}
