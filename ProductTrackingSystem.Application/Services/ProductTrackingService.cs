using AutoMapper;
using ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Domain.Repositories;
using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Application.Services
{
    public class ProductTrackingService : IProductTrackingService
    {
        private readonly IProductTrackingRepository _repository;
        private readonly IMapper _mapper;

        public ProductTrackingService(
            IProductTrackingRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(CreateProductTrackingLogDto productTrackingLogDto)
        {
            var log = _mapper.Map<ProductTrackingLog>(productTrackingLogDto);
            log.ActionDate = DateTime.Now;
            await _repository.AddAsync(log);
        }

        public async Task<IEnumerable<ProductTrackingLogDto>> GetLogsByProductIdAsync(int id)
        {
            var logs = await _repository.GetLogsByProductIdAsync(id);
            return _mapper.Map<IEnumerable<ProductTrackingLogDto>>(logs);
        }
    }
}
