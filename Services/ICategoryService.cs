using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface ICategoryService
    {
      public Task<ResCategoryDto> CreateCategoryAsync(ReqCategoryDto reqCategoryDto);
      public Task<ResCategoryDto?> UpdateCategoryAsync(long id, ReqCategoryDto reqCategoryDto);
      public Task<ResCategoryDto?> GetCategoryByIdAsync(long id);
      public Task<List<ResCategoryDto>> GetAllCategoryAsync();
      public Task<ResCategoryDto> DeleteCategoryAsync(long id);

    }
}
