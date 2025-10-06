using Microsoft.EntityFrameworkCore;
using ProductTrackingSystem.Application.Interfaces;
using ProductTrackingSystem.Application.Mapper;
using ProductTrackingSystem.Application.Services;
using ProductTrackingSystem.Domain.Repositories;
using ProductTrackingSystem.Infrastructure.Contexts;
using ProductTrackingSystem.Infrastructure.Repositories;

namespace ProductTrackingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Alloworigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<PTSContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddAutoMapper(typeof(MapConfig).Assembly);

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();
            builder.Services.AddScoped<IOrderLineService, OrderLineService>();

            builder.Services.AddScoped<IProductTrackingRepository, ProductTrackingRepository>();
            builder.Services.AddScoped<IProductTrackingService, ProductTrackingService>();
            

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }   

            app.UseCors("Alloworigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
