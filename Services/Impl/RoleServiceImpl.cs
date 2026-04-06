using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class RoleServiceImpl: IRoleService
    {
        private readonly MyAppContext _context;
        public RoleServiceImpl(MyAppContext context)
        {
            _context = context;
        }
        public async Task<ResRoleDto> CreateRole(ReqRoleDto reqRoleDto)
        {
            Role role = new Role()
            {
                RoleName = reqRoleDto.RoleName
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return new ResRoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
        public async Task<ResRoleDto> GetRoleById(long id)
        {
            Role role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
            if(role == null)
            {
                throw new Exception($"Role with Id = {id} not found");
            }
            return new ResRoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
        public async Task<List<ResRoleDto>> GetAllRole()
        {
            List<Role> roles = await _context.Roles
                .ToListAsync();
            var result = roles.Select(r => new ResRoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName
            }).ToList();
            return result;
        }
        public async Task<ResRoleDto> UpdateRole(long id, ReqRoleDto reqRoleDto)
        {
            Role role = await _context.Roles
             .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                throw new Exception($"Role with Id = {id} not found");
            }
            role.RoleName = role.RoleName;
            await _context.SaveChangesAsync();
            return new ResRoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
        
        public async Task<ResRoleDto> DeleteRole(long id)
        {
            Role role = await _context.Roles
             .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                throw new Exception($"Role with Id = {id} not found");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return new ResRoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
    }
}
