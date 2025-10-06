using AutoMapper;
using ProductTrackingSystem.Application.DTOs.OrderDTOs;
using ProductTrackingSystem.Application.DTOs.OrderLineDTOs;
using ProductTrackingSystem.Application.DTOs.ProductDTOS;
using ProductTrackingSystem.Application.DTOs.ProductTrackingDTOs;
using ProductTrackingSystem.Domain.Entities;

namespace ProductTrackingSystem.Application.Mapper
{
    public class MapConfig: Profile
    {
        public MapConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();


            CreateMap<Order, OrderDto>().ReverseMap();  
            CreateMap<Order, CreateOrderDto>().ReverseMap()
            .ForMember(dest => dest.OrderLines, opt => opt.Ignore());
            CreateMap<Order, UpdateOrderDto>().ReverseMap();


            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
            CreateMap<OrderLine, CreateOrderLineDto>().ReverseMap();
            CreateMap<OrderLine, UpdateOrderLineDto>().ReverseMap();

            CreateMap<ProductTrackingLog,ProductTrackingLogDto>().ReverseMap();
            CreateMap<CreateProductTrackingLogDto,ProductTrackingLog>();
        }
        }
}
