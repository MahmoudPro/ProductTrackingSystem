
using ProductTrackingSystem.Application.DTOs.ProductDTOS;

namespace ProductTrackingSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto createProductDto);
        Task<ProductDto?> GetProductBySkuAsync(string sku);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> IsSkuTakenAsync(string sku, int? productId = null);
        Task<bool> DeleteProductAsync(int id);

    }
}
