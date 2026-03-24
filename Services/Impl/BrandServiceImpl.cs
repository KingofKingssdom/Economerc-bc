using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Util;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class BrandServiceImpl: IBrandService   
    {
        private readonly MyAppContext _context;
        private readonly FileStorageUtil _fileStorageUtil;

        public BrandServiceImpl(MyAppContext context, FileStorageUtil fileStorage)
        {
            _context = context;
            _fileStorageUtil = fileStorage;
        }
        public async Task<ResBrandDto> CreateBrandAsync(ReqBrandDto reqBrandDto)
        {
            string? imagePath = await _fileStorageUtil.UploadImage(reqBrandDto.UrlImageBrand, "Brands");
            Brand brand = new Brand

            {
                brandCode = reqBrandDto.brandCode,
                brandName = reqBrandDto.brandName,
                UrlImageBrand = imagePath
            };
            bool isExist = await _context.Brands.AnyAsync(c => c.brandCode == reqBrandDto.brandCode);
                
            if (isExist)
            {
                throw new Exception("Brand code already exist!");
            }
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return new ResBrandDto
            {
                Id = brand.Id,
                brandCode = brand.brandCode,
                brandName = brand.brandName,
                UrlImageBrand = brand.UrlImageBrand
            };
        }

        public async Task<List<ResBrandDto>> GetAllBrandAsync()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();
            var result = brands.Select(brand => new ResBrandDto
            {
                Id = brand.Id,
                brandCode = brand.brandCode,
                brandName = brand.brandName,
                UrlImageBrand = brand.UrlImageBrand

            }).ToList();
            return result;
        }
        public async Task<ResBrandDto> GetBrandByBrandCodeAsync(string brandCode)
        {
            Brand? brand = await _context.Brands.
                FirstOrDefaultAsync(b => b.brandCode == brandCode);
            if(brand == null)
            {
                throw new Exception($"Brand with brand code = {brandCode} not found ");
            }
            return new ResBrandDto
            {
                Id = brand.Id,
                brandCode = brand.brandCode,
                brandName = brand.brandName
            };
        }
            public async Task<ResBrandDto> UpdateBrandAsync(string brandCode, ReqBrandDto reqBrandDto)
            {
                Brand? brand = await _context.Brands.
                    FirstOrDefaultAsync(b => b.brandCode == brandCode);
                if(brand == null)
                {
                    throw new Exception($"Brand with brand code = {brandCode} not found");
                }
                bool isExist = await _context.Brands.AnyAsync(b => 
                b.brandCode == reqBrandDto.brandCode && b.Id != brand.Id);

                if (isExist)
                {
                    throw new Exception($"Brand code already exist");
                }
                if (!string.IsNullOrEmpty(reqBrandDto.brandCode))
                {
                    brand.brandCode = reqBrandDto.brandCode;
                }
                if(!string.IsNullOrEmpty(reqBrandDto.brandName))
                {               
                    brand.brandName = reqBrandDto.brandName;
                }
                if (reqBrandDto.UrlImageBrand != null && reqBrandDto.UrlImageBrand.Length > 0)
                {
                    string? imagePath = await _fileStorageUtil.UploadImage(reqBrandDto.UrlImageBrand, "Brands");
                    brand.UrlImageBrand = imagePath;
                }
           
                await _context.SaveChangesAsync();
            var updatedBrand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == brand.Id);
            Console.WriteLine($"Gia tri sau khi Save: {brand.UrlImageBrand}");
            return new ResBrandDto
                {
                    Id = updatedBrand.Id,
                    brandCode = updatedBrand.brandCode,
                    brandName = updatedBrand.brandName,
                    UrlImageBrand = updatedBrand.UrlImageBrand
                
                };
            }

        public async Task<ResBrandDto> DeleteBrandAsync(string brandCode)
        {
            Brand? brand = await _context.Brands.
                FirstOrDefaultAsync(b => b.brandCode == brandCode);
            if(brand == null)
            {
                throw new Exception($"Brand with Id = {brandCode} not found");
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return new ResBrandDto
            {
                Id = brand.Id,
                brandCode = brand.brandCode,
                brandName = brand.brandName,
                UrlImageBrand = brand.UrlImageBrand
            };
        }
    }
}
