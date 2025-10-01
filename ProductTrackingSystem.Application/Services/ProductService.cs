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
        public ProductService(IProductRepository _productRepository, IMapper _mapper) {
            productRepository = _productRepository;
            mapper = _mapper;
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
            return productDto;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;

            mapper.Map(updateProductDto, existingProduct);

            var success = await productRepository.UpdateAsync(existingProduct);
            if (!success)
                return null;

            return mapper.Map<ProductDto>(existingProduct);
        }

        public async Task<ProductDto?> GetProductBySkuAsync(string sku)
        {
            var product =await productRepository.GetBySkuAsync(sku);
          var productDto = mapper.Map<ProductDto>(product);
            return productDto;

        }
    }
}
