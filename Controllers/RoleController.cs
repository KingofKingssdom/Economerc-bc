using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/role")]
    [Authorize(Roles = "Admin")]
    public class RoleController:ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<ActionResult<ResRoleDto>> Create(ReqRoleDto reqRoleDto)
        {
            var role = await _roleService.CreateRole(reqRoleDto);
            return Ok(new
            {
                message = "Create success",
                data = role
            });
        }
        [HttpGet]
        public async Task<ActionResult<ResRoleDto>> GetAll()
        {
            var role = await _roleService.GetAllRole();
            return Ok(new
            {
                message = "Get all role success",
                data = role
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResRoleDto>> GetById(long id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                return Ok(new
                {
                    message = "Get success",
                    data = role
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }


        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResRoleDto>> Update(long id, ReqRoleDto reqRoleDto)
        {
            try
            {
                var role = await _roleService.UpdateRole(id, reqRoleDto);
                return Ok(new
                {
                    message = "Update success",
                    data = role
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResRoleDto>> Delete(long id)
        {
            try
            {
                var role = await _roleService.DeleteRole(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = role
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
