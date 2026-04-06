using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface ISpecificationDetailService
    {
        public Task<List<ResSpecificationDetailDto>> CreateSpecificationDetail(List<ReqSpecificationDetailDto> reqSpecificationDetailDto);
        public Task<ResSpecificationDetailDto> GetSpecificationDetailById(long id);
        public Task<List<ResSpecificationDetailDto>> GetAllSpecificationDetail();
        public Task<List<ResSpecificationDetailDto>> UpdateSpecificationDetail(long productId, long productSpecificationId,ReqSpecificationDetailDto reqSpecificationDetailDto);
        public Task<ResSpecificationDetailDto> DeleteSpecificationDetail(long id);
    }
}
