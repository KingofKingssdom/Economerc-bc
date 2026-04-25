using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(ReqLoginDto reqLoginDto)
        {
            try
            {
                var responseToken = await _authService.LoginUser(reqLoginDto);
                return Ok(new
                {
                    message = "Login success!",
                    accessToken = responseToken.AccessToken,
                    reFreshToken = responseToken.RefreshToken
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
               
        }
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> Refresh(ReqRefreshTokenDto reqRefreshTokenDto)
        {
            var responseToken = await _authService.RefreshToken(
                reqRefreshTokenDto.oldAccessToken,
                reqRefreshTokenDto.oldRefreshToken
                );
            return Ok(new
            {
                message = "Refresh success",
                accessToken = responseToken.NewAccessToken,
                reFreshToken = responseToken.NewRefreshToken

            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var email = User.Identity.Name;
            var logout = await _authService.LogoutUser(email);
            return Ok(new
            {
                message = "Logout success!"
            });
        }
    }
}
