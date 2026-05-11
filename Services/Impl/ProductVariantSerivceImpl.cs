using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Collections;

namespace Ecommerce.Services.Impl
{
    public class ProductVariantSerivceImpl: IProductVariantService
    {
        private readonly MyAppContext _context;
        private readonly FileStorageUtil _fileStorageUtil;
        public ProductVariantSerivceImpl(MyAppContext context, FileStorageUtil fileStorageUtil)
        {
            _context = context;
            _fileStorageUtil = fileStorageUtil;
        }
        public async Task<ResProductVariantDto> CreateProductVariant(ReqProductVariantDto reqProductVariantDto, long productId)
        {
            string? urlImageProductColor = await _fileStorageUtil.UploadImage(reqProductVariantDto.UrlProductColor, "ProductColor");
            ProductVariant productVariant = new ProductVariant()
            {
                Storage = reqProductVariantDto.Storage,
                OriginPrice = reqProductVariantDto.OriginPrice,
                CurrentPrice = reqProductVariantDto.CurrentPrice,
                ProductId = productId,
                UrlProductColor = urlImageProductColor,
                ColorName = reqProductVariantDto.ColorName,
                Stock = reqProductVariantDto.Stock

            };
            await _context.ProductVariants.AddAsync(productVariant);
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
               Storage = productVariant.Storage,
               OriginPrice = productVariant.OriginPrice,
               CurrentPrice = productVariant.CurrentPrice,
               ProductId = productVariant.ProductId,
               Stock = productVariant.Stock,
               UrlProductColor = productVariant.UrlProductColor,
               ColorName = productVariant.ColorName
            
            };
            return resProductVariant;
        }
        public async Task<ResProductVariantDto> GetProductVariantById(long id) 
        {
            ProductVariant productVariant = await _context.ProductVariants
             .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with id {id} not found");
            }
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                OriginPrice = productVariant.OriginPrice,
                CurrentPrice = productVariant.CurrentPrice,
                ProductId = productVariant.ProductId,
                UrlProductColor = productVariant.UrlProductColor,
                Stock = productVariant.Stock,
                ColorName = productVariant.ColorName
                
            };
            return resProductVariant;

        } 
        public async Task<List<ResProductVariantDto>> GetAllProductVariant()
        {

            List<ProductVariant> productVariants = await _context.ProductVariants
                .ToListAsync();
            var result = productVariants.Select(pv => new ResProductVariantDto
            {
                Id = pv.Id,
                Storage = pv.Storage,
                OriginPrice = pv.OriginPrice,
                CurrentPrice = pv.CurrentPrice,
                ProductId = pv.ProductId,
                UrlProductColor = pv.UrlProductColor,
                ColorName = pv.ColorName,
                Stock = pv.Stock
            }).ToList();
            return result;
        }
        public async Task<ResProductVariantDto> UpdateProductVariant(long id, long productId ,ReqProductVariantDto reqProductVariantDto)
        {
            string? urlImageProductColor = await _fileStorageUtil.UploadImage(reqProductVariantDto.UrlProductColor, "ProductColor");
            ProductVariant productVariant = await _context.ProductVariants
            .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with id {id} not found");
            }

            productVariant.Storage = reqProductVariantDto.Storage;
            productVariant.OriginPrice = reqProductVariantDto.OriginPrice;
            productVariant.CurrentPrice = reqProductVariantDto.CurrentPrice;
            productVariant.ProductId = productId;
            productVariant.Stock = reqProductVariantDto.Stock;
            productVariant.UrlProductColor = urlImageProductColor;
            productVariant.ColorName = reqProductVariantDto.ColorName;
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                OriginPrice = productVariant.OriginPrice,
                CurrentPrice = productVariant.CurrentPrice,
                ProductId = productVariant.ProductId,
                UrlProductColor = productVariant.UrlProductColor,
                ColorName = productVariant.ColorName,
                Stock = productVariant.Stock
            };
            return resProductVariant;
        }
        public async Task<ResProductVariantDto> DeleteProductVariant(long id)
        {
            ProductVariant productVariant = await _context.ProductVariants
          .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with id {id} not found");
            }
            _context.ProductVariants.Remove(productVariant);
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                OriginPrice = productVariant.OriginPrice,
                CurrentPrice = productVariant.OriginPrice,
                ProductId = productVariant.ProductId,
                UrlProductColor = productVariant.UrlProductColor,
                ColorName = productVariant.ColorName,
                Stock = productVariant.Stock
            };
            return resProductVariant;
        }
        public async Task<List<ResProductVariantDto>> GetAllProductVariantByProductId(long productId)
        {
            var productVariants = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
            var resProductVariants = productVariants.Select(pv => new ResProductVariantDto
            {
                Id = pv.Id,
                Storage = pv.Storage,
                OriginPrice = pv.OriginPrice,
                CurrentPrice = pv.CurrentPrice,
                ProductId = pv.ProductId,
                UrlProductColor = pv.UrlProductColor,
                ColorName = pv.ColorName,
                Stock = pv.Stock
            }).ToList();
            return resProductVariants;
        }
    }
}
