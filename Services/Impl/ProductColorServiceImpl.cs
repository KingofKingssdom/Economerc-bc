using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class ProductColorServiceImpl: IProductColorService
    {
        private readonly MyAppContext _context;
        private readonly FileStorageUtil _fileStorageUtil;
        public ProductColorServiceImpl(MyAppContext context, FileStorageUtil fileStorageUtil)
        {
            _context = context;
            _fileStorageUtil = fileStorageUtil;
        }

        
        public async Task<ResProductColorDto> CreateProductColor(ReqProductColorDto reqProductColorDto) {
            string? urlImageProductColor = await _fileStorageUtil.UploadImage(reqProductColorDto.UrlProductColor, "ProductColor");
            ProductColor productColor = new ProductColor()
            {
                UrlProductColor = urlImageProductColor,
                ColorName = reqProductColorDto.ColorName,
                ProductId = reqProductColorDto.ProductId
            };
            await _context.ProductColors.AddAsync(productColor);
            await _context.SaveChangesAsync();
            ResProductColorDto resProductColor = new ResProductColorDto()
            {
                Id = productColor.Id,
                ColorName = productColor.ColorName,
                UrlProductColor = productColor.UrlProductColor,
                ProductId = productColor.ProductId
            };
            return resProductColor;
        }
        public async Task<ResProductColorDto> GetProductColorById(long id) {
            ProductColor productColor = await _context.ProductColors
             .FirstOrDefaultAsync(pc => pc.Id == id);
            if (productColor == null)
            {
                throw new Exception($"Product color with Id = {id} not found");
            }
            ResProductColorDto resProductColor = new ResProductColorDto()
            {
                Id = productColor.Id,
                ColorName = productColor.ColorName,
                UrlProductColor = productColor.UrlProductColor,
                ProductId = productColor.ProductId
            };
            return resProductColor;
        }
        public async Task<List<ResProductColorDto>> GetAllProductColor() {
            List<ProductColor> productColors = await _context.ProductColors
                .ToListAsync();
            var result = productColors.Select(pc => new ResProductColorDto
            {
                Id = pc.Id,
                ColorName = pc.ColorName,
                UrlProductColor = pc.UrlProductColor,
                ProductId = pc.ProductId
            }).ToList();
            return result;
            
        }
        public async Task<ResProductColorDto> UpdateProductColor(long id, ReqProductColorDto reqProductColor) {
            ProductColor productColor = await _context.ProductColors
            .FirstOrDefaultAsync(pc => pc.Id == id);
            if (productColor == null)
            {
                throw new Exception($"Product color with Id = {id} not found");
            }
            string? oldImagePath = productColor.UrlProductColor;
            string? urlImageProductColor = await _fileStorageUtil
                .UploadImage(reqProductColor.UrlProductColor, "ProductColors");
            if (urlImageProductColor != null)
            {
                productColor.UrlProductColor = urlImageProductColor;
                _fileStorageUtil.DeleteImage(oldImagePath);
            }
            productColor.ColorName = reqProductColor.ColorName;
            productColor.ProductId = reqProductColor.ProductId;
            await _context.SaveChangesAsync();
            ResProductColorDto resProductColor = new ResProductColorDto()
            {
                Id = productColor.Id,
                ColorName = productColor.ColorName,
                UrlProductColor = productColor.UrlProductColor,
                ProductId = productColor.ProductId
            };
            return resProductColor;
        }
        public async Task<ResProductColorDto> DeleteProductColor(long id) {
            ProductColor productColor = await _context.ProductColors
          .FirstOrDefaultAsync(pc => pc.Id == id);
            if(productColor == null)
            {
                throw new Exception($"Product color with Id = {id} not found");
            }
            _context.ProductColors.Remove(productColor);
            await _context.SaveChangesAsync();
            ResProductColorDto resProductColor = new ResProductColorDto()
            {
                Id = productColor.Id,
                ColorName = productColor.ColorName,
                UrlProductColor = productColor.UrlProductColor,
                ProductId = productColor.ProductId
            };
            return resProductColor;
        }

    }
}
