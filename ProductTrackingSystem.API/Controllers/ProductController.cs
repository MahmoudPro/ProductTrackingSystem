using Microsoft.AspNetCore.Mvc;
using ProductTrackingSystem.Application.DTOs.ProductDTOS;
using ProductTrackingSystem.Application.Interfaces;

namespace ProductTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                if (products == null || !products.Any())
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "No products found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Products retrieved successfully",
                    data = products
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while retrieving products"
                });
            }
        }


        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "Product not found"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Product retrieved successfully",
                    data = product
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while retrieving the product"
                });
            }
        }

        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
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
                var existingProduct = await _productService.GetProductBySkuAsync(createProductDto.SKU);
                if (existingProduct != null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        message = "A product with the same SKU already exists"
                    });
                }
                var product = await _productService.AddProductAsync(createProductDto);
                return Ok(new
                {
                    Success = true,
                    message = "Product added successfully",
                    data = product
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while adding the product"
                });
            }
        }

        
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
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

                var product = await _productService.GetProductByIdAsync(id);
                
                if (!string.Equals(product.SKU, updateProductDto.SKU, StringComparison.OrdinalIgnoreCase))
                {
                    var skuExists = await _productService.IsSkuTakenAsync(updateProductDto.SKU, id);
                    if (skuExists)
                    {
                        return BadRequest(new
                        {
                            Success = false,
                            message = "A product with the same SKU already exists"
                        });
                    }
                }

                var updatedProduct = await _productService.UpdateProductAsync(id, updateProductDto);
                if (!updatedProduct)
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "Product not found or update failed"
                    });
                }
                return Ok(new
                {
                    Success = true, 
                    message = "Product updated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while updating the product"
                });
            }
        }
        
        
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var success = await _productService.DeleteProductAsync(id);
                if (!success)
                {
                    return NotFound(new
                    {
                        Success = false,
                        message = "Product not found "
                    });
                }
                return Ok(new
                {
                    Success = true,
                    message = "Product deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    message = "An error occurred while deleting the product"
                });
            }

        }
    }
}
