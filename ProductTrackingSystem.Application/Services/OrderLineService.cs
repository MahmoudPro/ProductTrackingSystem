using AutoMapper;
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Application.Services
{
    public class OrderLineService: IOrderLineService
    {
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IMapper _mapper;
        public OrderLineService(IOrderLineRepository orderLineRepository, IMapper mapper)
        {
            _orderLineRepository = orderLineRepository;
            _mapper = mapper;
        }

        public async Task<OrderLineDto> AddOrderLineAsync(CreateOrderLineDto createOrderLineDto)
        {
            var orderline= _mapper.Map<OrderLine>(createOrderLineDto);
            var newOrderLine = await _orderLineRepository.AddAsync(orderline);
            var orderLineDto = _mapper.Map<OrderLineDto>(newOrderLine);
            return orderLineDto;

        }
        public async Task<bool> DeleteOrderLineAsync(int id)
        {
            return await _orderLineRepository.Delete(id);
        }


        public async Task<IEnumerable<OrderLineDto>> GetAllOrderLinesAsync()
        {
            var orderLines = await _orderLineRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderLineDto>>(orderLines);
        }

        public async Task<OrderLineDto> GetOrderLineByIdAsync(int id)
        {
            var orderLine = await _orderLineRepository.GetByIdAsync(id);
            return _mapper.Map<OrderLineDto>(orderLine);
        }

        public async Task<IEnumerable<OrderLineDto>> GetOrderLinesByOrderIdAsync(int orderId)
        {
            var orderLines = await _orderLineRepository.GetOrderLinesByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderLineDto>>(orderLines);
        }

        public async Task<bool> UpdateOrderLineAsync(int id, UpdateOrderLineDto updateOrderLineDto)
        {
            var existingOrderLine = await _orderLineRepository.GetByIdAsync(id);
            if (existingOrderLine == null)
                return false;

            _mapper.Map(updateOrderLineDto, existingOrderLine);
            return await _orderLineRepository.Update(existingOrderLine);
        }

    }
}
