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
            if(urlImageProduct == null) { throw new Exception("Url Image Product null"); }

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
            foreach (var productSpecificationId in reqProductDto.ProductSpecificationId)
            {
                product.ProductSpecifications.Add(new ProductSpecificationMapping
                {
                    ProductSpecificationId = productSpecificationId
                });
            }
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
                        .Include(ps=>ps.ProductSpecifications)
                        .ThenInclude(ps=>ps.ProductSpecification)
                        .ToListAsync();
            
            var resProduct = products.Select(p => new ResProductDto
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
                    BrandCode = p.Brand.BrandCode,
                    BrandName = p.Brand.BrandName,
                    UrlImageBrand = p.Brand.UrlImageBrand
                },
                ResProductSpecification = p.ProductSpecifications
                .Select(ps=> new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()
                
            }).ToList();
            return resProduct;
                
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
                    BrandCode = product.Brand.BrandCode,
                    BrandName = product.Brand.BrandName
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                },
                ResProductSpecification = product.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()
            };
            return ResProduct;
        }
        public async Task<ResProductDto> UpdateProduct(long id, ReqProductDto reqProductDto) {
            Product? product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.Brand)
                .Include(ps=>ps.ProductSpecifications)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                throw new Exception($"Product is id ={id} not found");
            }
          
            if(reqProductDto.UrlImageProduct != null)
            {
                product.UrlImageProduct = await _fileStorageUtil.UploadImage(reqProductDto.UrlImageProduct, "Products");
            }
            product.ProductSpecifications.Clear();
            product.ProductCode = reqProductDto.ProductCode;
            product.ProductName = reqProductDto.ProductName;
            product.Description = reqProductDto.Description;
            product.IsFeatured = reqProductDto.IsFeatured;
            product.IsOnPromotion = reqProductDto.IsOnPromotion;
            product.BrandId = reqProductDto.BrandId;
            product.CategoryId = reqProductDto.CategoryId;
            foreach (var productSpecificationId in reqProductDto.ProductSpecificationId)
            {
                product.ProductSpecifications.Add(new ProductSpecificationMapping
                {
                    ProductSpecificationId = productSpecificationId
                });
            }
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
                    BrandCode = product.Brand.BrandCode,
                    BrandName = product.Brand.BrandName
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                },
                ResProductSpecification = product.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            };
            return resProduct;
        }
        public async Task<ResProductDto> DeleteProduct(long id)
        {
            Product product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.Brand)
                .FirstOrDefaultAsync(p=> p.Id == id);
            if(product == null)
            {
                throw new Exception($"Product with product code = {id} not found");

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
                    BrandCode = product.Brand.BrandCode,
                    BrandName = product.Brand.BrandName
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
