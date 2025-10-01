using AutoMapper;
using ProductTrackingSystem.Application.DTOs.OrderDTOs;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;
using ProductTrackingSystem.Infrastructure.Repositories;

namespace ProductTrackingSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderLineRepository _orderLineRepository ;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository, IOrderLineRepository orderLineRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderLineRepository = orderLineRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersWithOrderLinesAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto?>(order);
        }
        public async Task<(bool Success, string? Message)> CheckStockAsync(CreateOrderDto createOrderDto)
        {
            foreach (var line in createOrderDto.OrderLines)
            {
                var product = await _productRepository.GetByIdAsync(line.ProductId);
                if (product == null)
                {
                    return (false, $"Product with ID {line.ProductId} not found");
                }
                if (product.QuantityInStock < line.Quantity)
                {
                    return (false, $"Insufficient stock for product {product.Name}");
                }
            }
            return (true, null);
        }

        public async Task<(bool Success, string? Message, OrderDto? Order)> PlaceOrderAsync(CreateOrderDto createOrderDto)
        {
            var (stockAvailable, message) = await CheckStockAsync(createOrderDto);
            if (!stockAvailable)
                return (false, message, null);

            var order = _mapper.Map<Order>(createOrderDto);
            order.TotalAmount = 0;

            var newOrder = await _orderRepository.AddAsync(order);

            foreach (var line in createOrderDto.OrderLines)
            {
                var product = await _productRepository.GetByIdAsync(line.ProductId);

                var orderLine = new OrderLine
                {
                    OrderId = newOrder.Id,
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                    UnitPrice = product.Price
                };

                await _orderLineRepository.AddAsync(orderLine);

                product.QuantityInStock -= line.Quantity;
                await _productRepository.UpdateAsync(product);

                order.TotalAmount += orderLine.Quantity * orderLine.UnitPrice;
            }

            await _orderRepository.UpdateAsync(order);

            return (true, "Order placed successfully", _mapper.Map<OrderDto>(order));
        }


        public async Task<bool> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return false;

            _mapper.Map(updateOrderDto, existingOrder);
            return await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
        
    }
}
