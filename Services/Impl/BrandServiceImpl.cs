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
            
            var existingBrand = await _context.Brands
                .Include(b => b.CategoryBrand) 
                .FirstOrDefaultAsync(b => b.BrandCode == reqBrandDto.BrandCode);

            Brand brand;

            if (existingBrand == null)
            {
                
                string? imagePath = await _fileStorageUtil.UploadImage(reqBrandDto.UrlImageBrand, "Brands");
                brand = new Brand
                {
                    BrandCode = reqBrandDto.BrandCode,
                    BrandName = reqBrandDto.BrandName,
                    UrlImageBrand = imagePath,
                    CategoryBrand = new List<CategoryBrand>()
                };
                await _context.Brands.AddAsync(brand);
            }
            else
            {
                brand = existingBrand;
                //brand.brandName = reqBrandDto.brandName;
            }

            
            if (reqBrandDto.CategoryIds != null)
            {
                foreach (var categoryId in reqBrandDto.CategoryIds)
                {
                   
                    bool alreadyCategoryId = brand.CategoryBrand.Any(cb => cb.CategoryId == categoryId);

                    if (!alreadyCategoryId)
                    {
                        var categoryBrand = new CategoryBrand
                        {
                            Brand = brand,
                            CategoryId = categoryId
                        };
                        brand.CategoryBrand.Add(categoryBrand);
                    }
                }
            }

            await _context.SaveChangesAsync();

            
            var brandWithDetails = await _context.Brands
                .Include(b => b.CategoryBrand)
                .ThenInclude(cb => cb.Category)
                .FirstOrDefaultAsync(b => b.Id == brand.Id);

            return new ResBrandDto
            {
                Id = brandWithDetails.Id,
                BrandCode = brandWithDetails.BrandCode,
                BrandName = brandWithDetails.BrandName,
                UrlImageBrand = brandWithDetails.UrlImageBrand,
                Categories = brandWithDetails.CategoryBrand.Select(c => new ResCategoryDto
                {
                    Id = c.CategoryId,
                    CategoryCode = c.Category.CategoryCode,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            };
        
        }

        public async Task<List<ResBrandDto>> GetAllBrandAsync()
        {
            var brands = await _context.Brands
                                .Include(b => b.CategoryBrand)           
                                .ThenInclude(cb => cb.Category)                            
                                .ToListAsync();
            var resBrand = brands.Select(brand => new ResBrandDto
            {
                Id = brand.Id,
                BrandCode = brand.BrandCode,
                BrandName = brand.BrandName,
                UrlImageBrand = brand.UrlImageBrand,
                Categories = brand.CategoryBrand.Select(cb => new ResCategoryDto
                {
                    Id = cb.CategoryId,
                    CategoryCode = cb.Category.CategoryCode,
                    CategoryName = cb.Category.CategoryName
                }).ToList()

            }).ToList();
            return resBrand;
        }
        public async Task<ResBrandDto> GetBrandByBrandCodeAsync(string brandCode)
        {
            Brand? brand = await _context.Brands
                .Include(b=>b.CategoryBrand)
                .ThenInclude(cb => cb.Category)
                .FirstOrDefaultAsync(b => b.BrandCode == brandCode);
            if(brand == null)
            {
                throw new Exception($"Brand with brand code = {brandCode} not found ");
            }
            return new ResBrandDto
            {
                Id = brand.Id,
                BrandCode = brand.BrandCode,
                BrandName = brand.BrandName,
                UrlImageBrand = brand.UrlImageBrand,
                Categories = brand.CategoryBrand.Select(cb => new ResCategoryDto
                {
                    Id = cb.CategoryId,
                    CategoryCode = cb.Category.CategoryCode,
                    CategoryName = cb.Category.CategoryName
                }).ToList()
            };
        }
            public async Task<ResBrandDto> UpdateBrandAsync(long id, ReqBrandDto reqBrandDto)
            {
                Brand? brand = await _context.Brands.
                    FirstOrDefaultAsync(b => b.Id == id);
                if(brand == null)
                {
                    throw new Exception($"Brand is id = {id} not found");
                }
                bool isExist = await _context.Brands.AnyAsync(b => 
                b.BrandCode == reqBrandDto.BrandCode && b.Id != brand.Id);

                if (isExist)
                {
                    throw new Exception($"Brand code already exist");
                }
                if (!string.IsNullOrEmpty(reqBrandDto.BrandCode))
                {
                    brand.BrandCode = reqBrandDto.BrandCode;
                }
                if(!string.IsNullOrEmpty(reqBrandDto.BrandName))
                {               
                    brand.BrandName = reqBrandDto.BrandName;
                }
                string? oldImagePath = brand.UrlImageBrand;
                string? imagePath = await _fileStorageUtil
                .UploadImage(reqBrandDto.UrlImageBrand, "Brands");
                if (imagePath != null)
                {
                brand.UrlImageBrand = imagePath;
                _fileStorageUtil.DeleteImage(oldImagePath);
                }
                await _context.SaveChangesAsync();
            var updatedBrand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == brand.Id);
            return new ResBrandDto
                {
                    Id = updatedBrand.Id,
                    BrandCode = updatedBrand.BrandCode,
                    BrandName = updatedBrand.BrandName,
                    UrlImageBrand = updatedBrand.UrlImageBrand
                
                };
            }

        public async Task<ResBrandDto> DeleteBrandAsync(long id)
        {
            Brand? brand = await _context.Brands.
                FirstOrDefaultAsync(b => b.Id == id);
            if(brand == null)
            {
                throw new Exception($"Brand with Id = {id} not found");
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return new ResBrandDto
            {
                Id = brand.Id,
                BrandCode = brand.BrandCode,
                BrandName = brand.BrandName,
                UrlImageBrand = brand.UrlImageBrand
            };
        }
    }
}
