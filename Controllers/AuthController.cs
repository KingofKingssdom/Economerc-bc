using Azure.Core;
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
                var accessCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                };
                var refreshCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/api/auth/refresh-token", 
                };
                Response.Cookies.Append("accessToken", responseToken.AccessToken, accessCookieOptions);
                Response.Cookies.Append("refreshToken", responseToken.RefreshToken, refreshCookieOptions);
                return Ok(new
                {
                    message = "Login success!"
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
        public async Task<ActionResult> Refresh()
        {
            var oldAccessToken = Request.Cookies["accessToken"];
            var oldRefreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(oldRefreshToken)) return Unauthorized("No refresh token");

            try
            {
                var responseToken = await _authService.RefreshToken(oldAccessToken, oldRefreshToken);

                var accessCookieOptions = new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None };
                var refreshCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/api/auth/refresh-token"
                };

                Response.Cookies.Append("accessToken", responseToken.NewAccessToken, accessCookieOptions);
                Response.Cookies.Append("refreshToken", responseToken.NewRefreshToken, refreshCookieOptions);

                return Ok(new { message = "Refresh success" });
            }
            catch
            {
                return Unauthorized("Token invalid or expired");
            }
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
