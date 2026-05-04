using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IBrandService
    {
        public Task<ResBrandDto> CreateBrandAsync(ReqBrandDto reqBrandDto);
        public Task<List<ResBrandDto>> GetAllBrandAsync();
        public Task<ResBrandDto> GetBrandByBrandCodeAsync(string brandCode);
        public Task<ResBrandDto> UpdateBrandAsync(long id, ReqBrandDto reqBrandDto);

        public Task<ResBrandDto> DeleteBrandAsync(long id);
        public Task<List<ResBrandDto>> GetAllBrandByCategoryId(long categoryId);

    }
}
