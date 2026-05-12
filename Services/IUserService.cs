using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        public Task<ResUserDto> CreateUser(ReqUserDto reqUserDto, string roleName);
        public Task<ResUserDto> GetUserById(long id);
        public Task<List<ResUserDto>> GetAllUser();
        public Task<ResUserDto> UpdateUser(long id, ReqUserDto reqUserDto, string roleName);
        public Task<ResUserDto> DeleteUser(long id);
        public Task<long> CountUser();
    }
}
