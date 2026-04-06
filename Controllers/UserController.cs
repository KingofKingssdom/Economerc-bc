using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("{roleName}")]
        public async Task<ActionResult<ResUserDto>> Create(ReqUserDto reqUserDto, string roleName)
        {
            try
            {
                var user = await _userService.CreateUser(reqUserDto, roleName);
                return Ok(new
                {
                    message = "Create success",
                    data = user
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            

        }
        [HttpGet]
        public async Task<ActionResult<ResUserDto>> GetAll()
        {
            var user = await _userService.GetAllUser();
            return Ok(new
            {
                message = "Get all user success",
                data = user
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResUserDto>> GetById(long id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(new
                {
                    message = "Get success",
                    data = user
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }


        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResUserDto>> Update(long id, ReqUserDto reqUserDto, string roleName)
        {
            try
            {
                var user = await _userService.UpdateUser(id, reqUserDto,roleName);
                return Ok(new
                {
                    message = "Update success",
                    data = user
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResUserDto>> Delete(long id)
        {
            try
            {
                var user = await _userService.DeleteUser(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = user
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
