using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface ICategoryService
    {
      public Task<ResCategoryDto> CreateCategory(ReqCategoryDto reqCategoryDto);
      public Task<ResCategoryDto?> UpdateCategory(long id, ReqCategoryDto reqCategoryDto);
      public Task<ResCategoryDto> GetCategoryByCategoryCode(string categoryCode);
      public Task<ResCategoryDto?> GetCategoryById(long id);
      public Task<List<ResCategoryDto>> GetAllCategory();
      public Task<ResCategoryDto> DeleteCategory(long id);

    }
}
