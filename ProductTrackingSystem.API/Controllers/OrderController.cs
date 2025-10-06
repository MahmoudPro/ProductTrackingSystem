using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTrackingSystem.Application.DTOs.OrderDTOs;
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;
using ProductTrackingSystem.Application.DTOs.ProductDTOS;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLineService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IOrderLineService orderLineService, IProductService productService)
        {
            _orderService = orderService;
            _orderLineService = orderLineService;
            _productService = productService;
        }
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetallOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                if (orders == null || !orders.Any())
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "No orders found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Orders retrieved successfully",
                    data = orders
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while retrieving orders"
                });
            }
        }
        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            try
            {
                var order=await _orderService.GetOrderByIdAsync(Id);
                if (order == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "No orders found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Orders retrieved successfully",
                    data = order
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while retrieving order"
                });
            }
        }
        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                return BadRequest(new
                {
                    Success = false,
                    message = "Invalid model state",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)

                });
                }
                var (success, message, order) = await _orderService.PlaceOrderAsync(createOrderDto);

                if (!success)
                return BadRequest(new 
                { 
                    Success = false,
                    message = message 
                });

                return Ok(new
                {
                    Success = true,
                    message = "Order placed successfully",
                    data = order
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while placing the order"
                });
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            try
            {
                var success = await _orderService.DeleteOrderAsync(Id);
                if (!success)
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "Order not found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Order deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while deleting the order"
                });
            }
        }

    }
}
