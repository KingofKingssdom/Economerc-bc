using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IProductColorService
    {
        public Task<ResProductColorDto> CreateProductColor(ReqProductColorDto reqProductColorDto);
        public Task<ResProductColorDto> GetProductColorById(long id);
        public Task<List<ResProductColorDto>> GetAllProductColor();
        public Task<ResProductColorDto> UpdateProductColor(long id, ReqProductColorDto reqProductColor);
        public Task<ResProductColorDto> DeleteProductColor(long id);
    }
}
