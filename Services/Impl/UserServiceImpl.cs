using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly MyAppContext _context;
        public UserServiceImpl(MyAppContext context)
        {
            _context = context; 
        }
        public async Task<ResUserDto> CreateUser(ReqUserDto reqUserDto, string roleName)
        {
         
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email == reqUserDto.Email);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null) throw new Exception("Role is not found");
            if (user == null)
            {
                var passwordHash = BCrypt.Net.
                BCrypt.HashPassword(reqUserDto.Password);

                 user = new User
                {
                    FullName = reqUserDto.FullName,
                    PhoneNumber = reqUserDto.PhoneNumber,
                    Email = reqUserDto.Email,
                    Password = passwordHash

                };
                await _context.Users.AddAsync(user);
            }
            else
            {
                
                bool alreadyHasRole = user.UserRoles.Any(ur => ur.RoleId == role.Id);
                if (alreadyHasRole)
                {
                    throw new Exception("This user already has this role.");
                }
            }
            UserRole userRole = new UserRole()
            {
                User = user,
                RoleId = role.Id
            };
            await _context.UserRoles.AddAsync(userRole);



            if (roleName == "Customer")
            {
                
                Cart cart = new Cart { User = user };
                _context.Carts.Add(cart);
                
            }
            await _context.SaveChangesAsync();
            return new ResUserDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ResRoleDtos = user.UserRoles.Select(ur => new ResRoleDto
                {
                    Id = ur.RoleId,
                    RoleName = ur.Role.RoleName
                }).ToList()
            };
        }
        public async Task<ResUserDto> GetUserById(long id)
        {
            User user = await _context.Users
                .Include(u=>u.UserRoles)
                .FirstOrDefaultAsync(u=> u.Id == id);
            if(user == null)
            {
                throw new Exception($"User has id = {id} not found");
            }
            return new ResUserDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ResRoleDtos = user.UserRoles.Select(ur => new ResRoleDto 
                { 
                    Id = ur.RoleId,
                    RoleName = ur.Role.RoleName
                }).ToList()
            };
        }
        public async Task<List<ResUserDto>> GetAllUser()
        {
            List<User> users = await _context.Users
                .Include(u=>u.UserRoles)
                .ToListAsync();
            var result = users.Select(u => new ResUserDto()
            {
                Id = u.Id,
                FullName = u.FullName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                ResRoleDtos = u.UserRoles.Select(ur => new ResRoleDto
                {
                    Id = ur.RoleId,
                    RoleName = ur.Role.RoleName
                }).ToList()
            }).ToList();
            return result;
        }
        public async Task<ResUserDto> UpdateUser(long id, ReqUserDto reqUserDto, string roleName)
        {
           
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) throw new Exception($"User with id={id} not found");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null) throw new Exception($"Role name is not found");

            var passwordHash = BCrypt.Net.
                BCrypt.HashPassword(reqUserDto.Password);
            user.FullName = reqUserDto.FullName;
            user.PhoneNumber = reqUserDto.PhoneNumber;
            user.Email = reqUserDto.Email;
            user.Password = passwordHash;



            _context.UserRoles.RemoveRange(user.UserRoles);

            
            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            await _context.SaveChangesAsync();

            
            return new ResUserDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
               
               
                ResRoleDtos = user.UserRoles.Select(ur => new ResRoleDto
                {
                    Id = ur.RoleId,
                    RoleName = ur.Role?.RoleName 
                }).ToList()
            };
        }
        public async Task<ResUserDto> DeleteUser(long id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u=>u.Id == id);
            if(user == null)
            {
                throw new Exception($"User is id = {id} not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ResUserDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }
        public async Task<long> CountUser()
        {
            var count = await _context.UserRoles
                .Where(ur => ur.RoleId == 2)
                .CountAsync();
            return count;
        }
    }
}
