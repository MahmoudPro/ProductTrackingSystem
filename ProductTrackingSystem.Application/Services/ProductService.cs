using AutoMapper;
using ProductTrackingSystem.Application.DTOs.ProductDTOS;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Domain.Entities;
using ProductTrackingSystem.Domain.Repositories;

namespace ProductTrackingSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductTrackingRepository productTracking;

        public ProductService(
            IProductRepository _productRepository,
            IMapper _mapper,
            IProductTrackingRepository _productTracking) {
            productRepository = _productRepository;
            mapper = _mapper;
            productTracking = _productTracking;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAll();
            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async  Task<ProductDto> AddProductAsync(CreateProductDto createProductDto)
        {
            var product = mapper.Map<Product>(createProductDto);
            var newProduct = await productRepository.AddAsync(product);
            var productDto = mapper.Map<ProductDto>(newProduct);

            string description = $"New product '{product.Name}' added with {product.QuantityInStock} units.";
            await LogActionAsync(
                    productDto.Id,
                    TrackingAction.Added,
                    productDto.QuantityInStock,
                    description
                );

            
            return productDto;

        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            var oldQuantity = existingProduct.QuantityInStock;
            var oldPrice = existingProduct.Price;

            mapper.Map(updateProductDto, existingProduct);

            var success = await productRepository.UpdateAsync(existingProduct);
            if (!success)
                return false;


            var quantityDiff = existingProduct.QuantityInStock - oldQuantity;
            var priceDiff = existingProduct.Price - oldPrice;
            TrackingAction actionType;
            string description;

            if (quantityDiff > 0)
            {
                actionType = TrackingAction.AddedStock;
                description = $"{quantityDiff} units added to stock.";
            }
            else if (quantityDiff < 0)
            {
                actionType = TrackingAction.RemovedStock;
                description = $"{Math.Abs(quantityDiff)} units removed from stock.";
            }
            else if (existingProduct.Price != oldPrice)
            {
                actionType = TrackingAction.Updated;
                description = $"Price updated from {oldPrice:C} to {existingProduct.Price:C}.";
            }
            else
            {
                actionType = TrackingAction.Updated;
                description = "Product details updated.";
            }

            await LogActionAsync(
                existingProduct.Id,
                actionType,
                quantityDiff,
                description
            );

            return true;
        }

        public async Task LogActionAsync(int productId,TrackingAction actionType, int quantityChange, string description)
        {
            var log = new ProductTrackingLog
            {
                ProductId = productId,
                Action = actionType,
                QuantityChange = quantityChange,
                Description = description,
                ActionDate = DateTime.UtcNow
            };

            await productTracking.AddAsync(log);

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto?> GetProductBySkuAsync(string sku)
        {
            var product =await productRepository.GetBySkuAsync(sku);
          var productDto = mapper.Map<ProductDto>(product);
            return productDto;

        }

        public async Task<bool> IsSkuTakenAsync(string sku, int? productId = null)
        {
            var product = await productRepository.GetBySkuAsync(sku);
            if (product == null)
                return false;
            if (productId.HasValue && product.Id == productId.Value)
                return false;
            return true;
        }

    }
}
