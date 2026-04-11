
using Ecommerce.Data;
using Ecommerce.Services;
using Ecommerce.Services.Impl;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
    
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<MyAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
            builder.Services.AddScoped<IBrandService, BrandServiceImpl>();
            builder.Services.AddScoped<IProductService, ProductServiceImpl>();
            builder.Services.AddScoped<IProductColorService, ProductColorServiceImpl>();
            builder.Services.AddScoped<IProductVariantService, ProductVariantSerivceImpl>();
            builder.Services.AddScoped<IProductSpecificationService, ProductSpecificationServiceIpml>();
            builder.Services.AddScoped<ISpecificationDetailService, SpecificationDetailServiceImpl>();
            builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
            builder.Services.AddScoped<ICartItemService, CartItemServiceImpl>();
            builder.Services.AddScoped<FileStorageUtil>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}
