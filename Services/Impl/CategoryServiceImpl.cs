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
       public async Task<ResCategoryDto> CreateCategory(ReqCategoryDto reqCategoryDto) {
         
            Category category = new Category
            {
                CategoryCode = reqCategoryDto.CategoryCode,
                CategoryName = reqCategoryDto.CategoryName

            };
            bool isExist = await _context.Categories
                .AnyAsync(c => c.CategoryCode == reqCategoryDto.CategoryCode);
            if (isExist)
            {
                throw new Exception("The category code already exists");
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
       public async Task<ResCategoryDto?> UpdateCategory(long id, ReqCategoryDto reqCategoryDto) {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            bool isExist = await _context.Categories
                .AnyAsync(c => c.CategoryCode == reqCategoryDto.CategoryCode && c.Id != id);
            if (isExist)
            {
                throw new Exception("The category code already exists");
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
        public async Task<ResCategoryDto?> GetCategoryById(long id) {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                throw new Exception($"Category with id {id} not found");
            }
            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
        }
        public async Task<List<ResCategoryDto>> GetAllCategory() {
            List<Category> categories = await _context.Categories.ToListAsync();
            var resCategory = categories.Select(caterogy => new ResCategoryDto
            {
                Id = caterogy.Id,
                CategoryCode = caterogy.CategoryCode,
                CategoryName = caterogy.CategoryName
            }).ToList();
            
            return resCategory;
        }
        public async Task<ResCategoryDto> DeleteCategory(long id) {

            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                throw new Exception($"Category with id {id} not found");
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
        public async Task<ResCategoryDto> GetCategoryByCategoryCode(string categoryCode)
        {
            Category category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryCode == categoryCode);
            if (category == null)
            {
                throw new Exception($"Category with category code {categoryCode} not found");
            }
            return new ResCategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
        }
    }
}
