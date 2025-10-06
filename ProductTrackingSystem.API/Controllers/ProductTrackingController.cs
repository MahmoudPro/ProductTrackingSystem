using Microsoft.AspNetCore.Mvc;
using ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs;
using ProductTrackingSystem.Application.Interfaces;

namespace ProductTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTrackingController : ControllerBase
    {
        private readonly IProductTrackingService _service;

        public ProductTrackingController(IProductTrackingService service)
        {
            _service = service;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult> GetLogs(int productId)
        {
            try
            {
                var logs = await _service.GetLogsByProductIdAsync(productId);
                if (logs == null || !logs.Any())
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "No logs found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "log retrieved successfully",
                    data = logs
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while retrieving logs"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLog([FromBody] CreateProductTrackingLogDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        message = "Invalid model state",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)

                    });

                await _service.AddAsync(dto);

                return Ok(new {
                    Success = true,
                    message = "Tracking log added successfully." 
                });

            }
            catch (Exception ex) {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while adding the log"
                });
            }
        }
    }
}
