using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IProductService
    {
        public Task<ResProductDto> CreateProduct(ReqProductDto reqProductDto);
        public Task<List<ResProductDto>> GetAllProduct();
        public Task<ResProductDto> GetByProductCode(string productCode);
        public Task<ResProductDto> UpdateProduct(long id, ReqProductDto reqProductDto);
        public Task<ResProductDto> DeleteProduct(long id);

    }
}
