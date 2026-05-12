using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IProductVariantService
    {
        public Task<ResProductVariantDto> CreateProductVariant(ReqProductVariantDto reqProductVariantDto, long productId);
        public Task<ResProductVariantDto> GetProductVariantById(long id);
        public Task<List<ResProductVariantDto>> GetAllProductVariant();
        public Task<ResProductVariantDto> UpdateProductVariant(long id, long productId ,ReqProductVariantDto reqProductVariantDto);
        public Task<ResProductVariantDto> DeleteProductVariant(long id);
        public Task<List<ResProductVariantDto>> GetAllProductVariantByProductId(long id);
        public Task<long> CountProduct();
    }
}
