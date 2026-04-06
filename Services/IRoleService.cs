using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IRoleService
    {
        public Task<ResRoleDto> CreateRole(ReqRoleDto reqRoleDto);
        public Task<ResRoleDto> GetRoleById(long id);
        public Task<List<ResRoleDto>> GetAllRole();
        public Task<ResRoleDto> UpdateRole(long id, ReqRoleDto reqRoleDto);
        public Task<ResRoleDto> DeleteRole(long id);
    }
}
