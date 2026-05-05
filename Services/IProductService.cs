using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IProductService
    {
        public Task<ResProductDto> CreateProduct(ReqProductDto reqProductDto);
        public Task<List<ResProductDto>> GetAllProduct();
        public Task<ResProductDto> GetByProductCode(string productCode);
        public Task<List<ResProductDto>> GetAllByProductByName(string productName);
        public Task<ResProductDto> UpdateProduct(long id, ReqProductDto reqProductDto);
        public Task<ResProductDto> DeleteProduct(long id);
        public Task<List<ResProductDto>> GetAllProductByIsFeatured(long categoryId);
        public Task<List<ResProductDto>> GetAllProductByIsOnPromotion();
        public Task<ResProductDto> GetProductById(long id);
        public Task<List<ResProductDto>> GetAllProductByCategoryId(long categoryId);
        public Task<List<ResProductDto>> GetAllProductByCategoryIdAndBrandId(long categoryId, long brandId);
    }
}
