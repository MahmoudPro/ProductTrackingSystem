using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs;

namespace ProductTrackingSystem.Application.Interfaces
{
    public interface IProductTrackingService
    {
        Task<IEnumerable<ProductTrackingLogDto>> GetLogsByProductIdAsync(int id);

        Task AddAsync(CreateProductTrackingLogDto productTrackingLogDto);
    }
}
