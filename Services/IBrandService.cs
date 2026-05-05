using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IBrandService
    {
        public Task<ResBrandDto> CreateBrand(ReqBrandDto reqBrandDto);
        public Task<List<ResBrandDto>> GetAllBrand();
        public Task<ResBrandDto> GetBrandByBrandCode(string brandCode);
        public Task<ResBrandDto> UpdateBrand(long id, ReqBrandDto reqBrandDto);

        public Task<ResBrandDto> DeleteBrand(long id);
        public Task<List<ResBrandDto>> GetAllBrandByCategoryId(long categoryId);

    }
}
