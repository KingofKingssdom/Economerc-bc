using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IProductSpecificationService
    {
        public Task<ResProductSpecificationDto> 
            CreateProductSpecification(ReqProductSpecificationDto reqProductSpecificationDto);
        public Task<ResProductSpecificationDto> GetProductSpecificationById(long id);
        public Task<List<ResProductSpecificationDto>> GetAllProductSpecification();
        public Task<ResProductSpecificationDto> 
            UpdateProductSpecification(long id, ReqProductSpecificationDto reqProductSpecificationDto);
        public Task<ResProductSpecificationDto> DeleteProductSpecification(long id);
    }
}
