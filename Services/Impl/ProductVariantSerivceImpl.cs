using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ResProductVariantDto> CreateProductVariant(ReqProductVariantDto reqProductVariantDto)
        {
            ProductVariant productVariant = new ProductVariant()
            {
                Storage = reqProductVariantDto.Storage,
                PriceOrigin = reqProductVariantDto.PriceOrigin,
                PriceDiscount = reqProductVariantDto.PriceDiscount,
                ProductId = reqProductVariantDto.ProductId
            };
            await _context.ProductVariants.AddAsync(productVariant);
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
               Storage = productVariant.Storage,
               PriceOrigin = productVariant.PriceOrigin,
               PriceDiscount = productVariant.PriceDiscount,
                ProductId = productVariant.ProductId
            };
            return resProductVariant;
        }
        public async Task<ResProductVariantDto> GetProductVariantById(long id) 
        {
            ProductVariant productVariant = await _context.ProductVariants
             .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with Id = {id} not found");
            }
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                PriceOrigin = productVariant.PriceOrigin,
                PriceDiscount = productVariant.PriceDiscount,
                ProductId = productVariant.ProductId
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
                PriceOrigin = pv.PriceOrigin,
                PriceDiscount = pv.PriceDiscount,
                ProductId = pv.ProductId
            }).ToList();
            return result;
        }
        public async Task<ResProductVariantDto> UpdateProductVariant(long id, ReqProductVariantDto reqProductVariantDto)
        {
            ProductVariant productVariant = await _context.ProductVariants
            .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with Id = {id} not found");
            }

            productVariant.Storage = reqProductVariantDto.Storage;
            productVariant.PriceOrigin = reqProductVariantDto.PriceOrigin;
            productVariant.PriceDiscount = productVariant.PriceDiscount;
            productVariant.ProductId = productVariant.ProductId;
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                PriceOrigin = productVariant.PriceOrigin,
                PriceDiscount = productVariant.PriceDiscount,
                ProductId = productVariant.ProductId
            };
            return resProductVariant;
        }
        public async Task<ResProductVariantDto> DeleteProductVariant(long id)
        {
            ProductVariant productVariant = await _context.ProductVariants
          .FirstOrDefaultAsync(pv => pv.Id == id);
            if (productVariant == null)
            {
                throw new Exception($"Product variant with Id = {id} not found");
            }
            _context.ProductVariants.Remove(productVariant);
            await _context.SaveChangesAsync();
            ResProductVariantDto resProductVariant = new ResProductVariantDto()
            {
                Id = productVariant.Id,
                Storage = productVariant.Storage,
                PriceOrigin = productVariant.PriceOrigin,
                PriceDiscount = productVariant.PriceDiscount,
                ProductId = productVariant.ProductId
            };
            return resProductVariant;
        }
    }
}
