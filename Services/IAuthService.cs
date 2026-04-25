using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IAuthService
    {
        public Task<ResLoginDto> LoginUser(ReqLoginDto reqLoginDto);
        public Task<string> LogoutUser(string email);
        public Task<ResNewToken> RefreshToken(string expiredAccessToken, string oldRefreshToke);
    }
}
