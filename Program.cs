
using Ecommerce.Config;
using Ecommerce.Data;
using Ecommerce.Services;
using Ecommerce.Services.Impl;
using Ecommerce.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
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
            builder.Services.AddScoped<IProductVariantService, ProductVariantSerivceImpl>();
            builder.Services.AddScoped<IProductSpecificationService, ProductSpecificationServiceIpml>();
            builder.Services.AddScoped<ISpecificationDetailService, SpecificationDetailServiceImpl>();
            builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
            builder.Services.AddScoped<ICartItemService, CartItemServiceImpl>();
            builder.Services.AddScoped<IOrderItemService, OrderItemServiceImpl>();
            builder.Services.AddScoped<FileStorageUtil>();
            builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // Chỉ dùng trong dev, nên bật true ở production
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        RoleClaimType = ClaimTypes.Role
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("accessToken"))
                            {
                                context.Token = context.Request.Cookies["accessToken"];
                            }
                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                   {
                       context.Response.StatusCode = 403;
                       context.Response.ContentType = "application/json";
                       return context.Response.WriteAsync("Bạn không có quyền truy cập vào đây!");
                   }
                    };
                }); 
            builder.Services.Configure<VNPayConfig>(
            builder.Configuration.GetSection("VNPayConfig")
            );
            var vnpaySettings = builder.Configuration.GetSection("VNPayConfig").Get<VNPayConfig>();
            builder.Services.AddSingleton(vnpaySettings);
            builder.Services.AddScoped<VNPayService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // Tuyệt đối không dùng .AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials(); // BẮT BUỘC phải có dòng này để sửa lỗi của bạn
                    });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseRouting();
            app.UseCors("AllowReactApp");
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
