using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class ProductServiceImpl: IProductService
    {
        private readonly MyAppContext _context;
        private readonly FileStorageUtil _fileStorageUtil;
        public ProductServiceImpl(MyAppContext context, FileStorageUtil fileStorage)
        {
            _context = context;
            _fileStorageUtil = fileStorage;
        }
        public async Task<ResProductDto> CreateProduct(ReqProductDto reqProductDto)
        {
            string? urlImageProduct = await _fileStorageUtil.UploadImage(reqProductDto.UrlImageProduct, "Products");
            Product product = new Product()
            {
                ProductCode = reqProductDto.ProductCode,
                ProductName = reqProductDto.ProductName,
                Description = reqProductDto.Description,
                UrlImageProduct = urlImageProduct,
                IsFeatured = reqProductDto.IsFeatured,
                IsOnPromotion = reqProductDto.IsOnPromotion,
                BrandId = reqProductDto.BrandId,
                CategoryId = reqProductDto.CategoryId
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new ResProductDto
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                UrlImageProduct = product.UrlImageProduct,
                IsFeatured = product.IsFeatured,
                IsOnPromotion = product.IsOnPromotion,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId
            };

            
        }
        public async Task<List<ResProductDto>> GetAllProduct()
        {
            List<Product> products = await _context.Products
                        .Include(p=>p.Category)
                        .Include(p=> p.Brand)
                        .ToListAsync();
            
            var result = products.Select(p => new ResProductDto
            {
                Id = p.Id,
                ProductCode = p.ProductCode,
                ProductName = p.ProductName,
                Description = p.Description,
                UrlImageProduct = p.UrlImageProduct,
                IsFeatured = p.IsFeatured,
                IsOnPromotion = p.IsOnPromotion,
                ResCategory = new ResCategoryDto
                {
                    Id = p.Category.Id,
                    CategoryCode = p.Category.CategoryCode,
                    CategoryName = p.Category.CategoryName
                },
                ResBrandDto = new ResBrandDto
                {
                    Id = p.Brand.Id,
                    brandCode = p.Brand.brandCode,
                    brandName = p.Brand.brandName,
                    UrlImageBrand = p.Brand.UrlImageBrand
                }
                
            }).ToList();
            return result;
                
        }
        public async Task<ResProductDto> GetByProductCode(string productCode)
        {
            Product? product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductCode == productCode);
            if(product == null)
            {
                throw new Exception($"Not found with product code = {productCode}");
            }
            
            var ResProduct = new ResProductDto
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                UrlImageProduct = product.UrlImageProduct,
                IsFeatured = product.IsFeatured,
                IsOnPromotion = product.IsOnPromotion,
                ResBrandDto = new ResBrandDto
                {
                    Id = product.Brand.Id,
                    brandCode = product.Brand.brandCode,
                    brandName = product.Brand.brandName
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                }

            };
            return ResProduct;
        }
        public async Task<ResProductDto> UpdateProduct(string productCode, ReqProductDto reqProductDto) {
            Product? product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.Brand)
                .FirstOrDefaultAsync(p => p.ProductCode == productCode);
            if(product == null)
            {
                throw new Exception($"Not found with product code = {productCode}");
            }
            bool isExist = await _context.Products
                .AnyAsync(p => p.ProductCode == productCode && p.Id != product.Id);
            if (isExist)
            {
                throw new Exception($"Product with productCode = {productCode} already");
            }
            if(reqProductDto.UrlImageProduct != null)
            {
                product.UrlImageProduct = await _fileStorageUtil.UploadImage(reqProductDto.UrlImageProduct, "Products");
            }
            
            product.ProductCode = reqProductDto.ProductCode;
            product.ProductName = reqProductDto.ProductName;
            product.Description = reqProductDto.Description;
            product.IsFeatured = reqProductDto.IsFeatured;
            product.IsOnPromotion = reqProductDto.IsOnPromotion;
            product.BrandId = reqProductDto.BrandId;
            product.CategoryId = reqProductDto.CategoryId;
            await _context.SaveChangesAsync();
            
            ResProductDto resProduct = new ResProductDto
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                UrlImageProduct = product.UrlImageProduct,
                IsFeatured = product.IsFeatured,
                IsOnPromotion = product.IsOnPromotion,
                ResBrandDto = new ResBrandDto
                {
                    Id = product.Brand.Id,
                    brandCode = product.Brand.brandCode,
                    brandName = product.Brand.brandName
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                }
            };
            return resProduct;
        }
        public async Task<ResProductDto> DeleteProduct(string productCode)
        {
            Product product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.Brand)
                .FirstOrDefaultAsync(p=> p.ProductCode == productCode);
            if(product == null)
            {
                throw new Exception($"Product with product code = {productCode} not found");

            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            ResProductDto resProduct = new ResProductDto
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                UrlImageProduct = product.UrlImageProduct,
                IsFeatured = product.IsFeatured,
                IsOnPromotion = product.IsOnPromotion,
                ResBrandDto = new ResBrandDto
                {
                    Id = product.Brand.Id,
                    brandCode = product.Brand.brandCode,
                    brandName = product.Brand.brandName
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                }
            };
            return resProduct;
        }

    }
}
