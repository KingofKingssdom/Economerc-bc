using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class CategoryServiceImpl: ICategoryService
       
    {
        private readonly MyAppContext _context;
        public CategoryServiceImpl(MyAppContext context)
        {
            _context = context;
        }
       public async Task<ResCategoryDto> CreateCategoryAsync(ReqCategoryDto reqCategoryDto) {
            Category category = new Category
            {
                CategoryCode = reqCategoryDto.CategoryCode,
                CategoryName = reqCategoryDto.CategoryName

            };
            bool isExist = await _context.Categories
                .AnyAsync(c => c.CategoryCode == reqCategoryDto.CategoryCode);
            if (isExist)
            {
                throw new Exception("Category code already exists");
            }
            await _context.Categories.AddAsync(category);     
            await _context.SaveChangesAsync();

            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
        }
       public async Task<ResCategoryDto?> UpdateCategoryAsync(long id, ReqCategoryDto reqCategoryDto) {
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            bool isExist = await _context.Categories
                .AnyAsync(c => c.CategoryCode == reqCategoryDto.CategoryCode && c.Id != id);
            if (isExist)
            {
                throw new Exception("Category code  already exists");
            }
            category.CategoryCode = reqCategoryDto.CategoryCode;
            category.CategoryName = reqCategoryDto.CategoryName;
            
            await _context.SaveChangesAsync();

            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };

        }
        public async Task<ResCategoryDto?> GetCategoryByIdAsync(long id) {
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return null;
            }
            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
        }
        public async Task<List<ResCategoryDto>> GetAllCategoryAsync() {
            List<Category> categories = await _context.Categories.ToListAsync();
            var result = categories.Select(caterogy => new ResCategoryDto
            {
                Id = caterogy.Id,
                CategoryCode = caterogy.CategoryCode,
                CategoryName = caterogy.CategoryName
            }).ToList();
            
            return result;
        }
        public async Task<ResCategoryDto> DeleteCategoryAsync(long id) {

            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                throw new Exception($"Category Id = {id} not found");
            }
             _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
           
        }
    }
}
