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
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var resProduct = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();
            return new ResProductDto
            {
                Id = resProduct.Id,
                ProductCode = resProduct.ProductCode,
                ProductName = resProduct.ProductName,
                Description = resProduct.Description,
                UrlImageProduct = resProduct.UrlImageProduct,
                IsFeatured = resProduct.IsFeatured,
                IsOnPromotion = resProduct.IsOnPromotion,
                ResBrandDto = new ResBrandDto()
                {
                    BrandCode = resProduct.Brand.BrandCode,
                    BrandName = resProduct.Brand.BrandName,
                    UrlImageBrand = resProduct.Brand.UrlImageBrand
                },
                ResCategory = new ResCategoryDto()
                {
                    CategoryCode = resProduct.Category.CategoryCode,
                    CategoryName = resProduct.Category.CategoryName
                }

            };

            
        }
        public async Task<List<ResProductDto>> GetAllProduct()
        {
            List<Product> products = await _context.Products
                        .Include(p=>p.Category)
                        .Include(p=> p.Brand)
                        .Include(p=> p.ProductVariants) 
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

                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName,
                    Stock = v.Stock

                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
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
                    .Include(p=>p.ProductVariants)
                    .FirstOrDefaultAsync(p => p.ProductCode == productCode);
            if(product == null)
            {
                throw new Exception($"Product with product code {productCode} not found");
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
                    BrandName = product.Brand.BrandName,
                    UrlImageBrand = product.Brand.UrlImageBrand
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                },
                ResProductVariantDto = product.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName
                }).ToList(),
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
                throw new Exception($"Product with id {id} not found");
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
            //foreach (var productSpecificationId in reqProductDto.ProductSpecificationId)
            //{
            //    product.ProductSpecifications.Add(new ProductSpecificationMapping
            //    {
            //        ProductSpecificationId = productSpecificationId
            //    });
            //}
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
                    BrandName = product.Brand.BrandName,
                    UrlImageBrand = product.Brand.UrlImageBrand
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
                throw new Exception($"Product with id {id} not found");

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
                    BrandName = product.Brand.BrandName,
                    UrlImageBrand = product.Brand.UrlImageBrand
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
        public async Task<List<ResProductDto>> GetAllProductByIsFeatured(long categoryId)
        {
            var product = await _context.Products
                .Where(p => p.IsFeatured == true && p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p=> p.ProductVariants)  
                .Include(p => p.ProductSpecifications)
                .ThenInclude(ps => ps.ProductSpecification)
                .ToListAsync();
            var resProduct = product.Select(p => new ResProductDto
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
                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName
                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            }).ToList();
            return resProduct;
        }
        public async Task<List<ResProductDto>> GetAllProductByIsOnPromotion()
        {
            var product = await _context.Products
                .Where(p => p.IsOnPromotion == true)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p=> p.ProductVariants)
                .Include(p => p.ProductSpecifications)
                .ThenInclude(ps => ps.ProductSpecification)
                .ToListAsync();
            var resProduct = product.Select(p => new ResProductDto
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
                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    ColorName = v.ColorName,
                    UrlProductColor = v.UrlProductColor
                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            }).ToList();
            return resProduct;
        }
        public  async Task<ResProductDto> GetProductById(long id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductSpecifications)
                .ThenInclude(ps => ps.ProductSpecification) 
                .FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                throw new Exception($"Product with id {id} not found");
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
                    BrandName = product.Brand.BrandName,
                    UrlImageBrand = product.Brand.UrlImageBrand
                },
                ResCategory = new ResCategoryDto
                {
                    Id = product.Category.Id,
                    CategoryCode = product.Category.CategoryCode,
                    CategoryName = product.Category.CategoryName
                },
                ResProductVariantDto = product.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock  = v.Stock,
                    ColorName = v.ColorName,
                    UrlProductColor = v.UrlProductColor
                }).ToList(),
                ResProductSpecification = product.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()
            };
            return ResProduct;

        }
        public async Task<List<ResProductDto>> GetAllByProductByName(string productName)
        {
            var product = await _context.Products
               .Where(p => p.ProductName.ToLower().Contains(productName.ToLower()))
               .Include(p => p.Category)
               .Include(p => p.Brand)
               .Include(p => p.ProductVariants)
               .Include(p => p.ProductSpecifications)
               .ThenInclude(ps => ps.ProductSpecification)
               .ToListAsync();
            if(product == null)
            {
                throw new Exception("Product is not found");
            }
            var resProduct = product.Select(p => new ResProductDto
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
                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName
                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            }).ToList();
            return resProduct;
        }

        public async Task<List<ResProductDto>> GetAllProductByCategoryId(long categoryId)
        {
            var product = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductSpecifications)
                .ThenInclude(ps => ps.ProductSpecification)
                .ToListAsync();
            if(product == null)
            {
                throw new Exception($"Product with categoryId {categoryId} not found");
            }
            var resProduct = product.Select(p => new ResProductDto
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
                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName
                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            }).ToList();

            return resProduct;
        }
        public async Task<List<ResProductDto>> GetAllProductByCategoryIdAndBrandId(long categoryId, long brandId)
        {
            var product = await _context.Products
                .Where(p => p.CategoryId == categoryId && p.BrandId == brandId)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductSpecifications)
                .ThenInclude(ps => ps.ProductSpecification)
                .ToListAsync();
            var resProduct = product.Select(p => new ResProductDto
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
                ResProductVariantDto = p.ProductVariants.Select(v => new ResProductVariantDto
                {
                    Id = v.Id,
                    CurrentPrice = v.CurrentPrice,
                    OriginPrice = v.OriginPrice,
                    Storage = v.Storage,
                    Stock = v.Stock,
                    UrlProductColor = v.UrlProductColor,
                    ColorName = v.ColorName
                }).ToList(),
                ResProductSpecification = p.ProductSpecifications
                .Select(ps => new ResProductSpecificationDto
                {

                    Id = ps.ProductSpecification.Id,
                    SpecificationName = ps.ProductSpecification.SpecificationName
                }).ToList()

            }).ToList();

            return resProduct;
        }
        

    }
}
