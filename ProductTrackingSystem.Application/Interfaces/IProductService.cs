
using ProductTrackingSystem.Application.DTOs.ProductDTOS;

namespace ProductTrackingSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<ProductDto?> GetProductBySkuAsync(string sku);
        Task<bool> DeleteProductAsync(int id);

    }
}
