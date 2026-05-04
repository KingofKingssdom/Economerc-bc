using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services.Impl
{
    public class AuthServiceImpl: IAuthService
    {
        private readonly MyAppContext _context;
        private readonly IConfiguration _configuration;
        public AuthServiceImpl(MyAppContext context, IConfiguration configuration) 
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ResLoginDto> LoginUser(ReqLoginDto reqLoginDto) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == reqLoginDto.Email);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(reqLoginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Password is incorrect");
            }
            var resUserDto = new ResUserDto
            {
                Email = reqLoginDto.Email,
                FullName = user.FullName
            };
            string accessToken = GenerateToken(resUserDto, out string jtiFromToken);
            var reFreshToken = new UserToken()
            {
                Token = GenerateRandomString(),
                UserId = user.Id,
                AddedDate = DateTime.Now,
                IsUsed = false,
                IsRevoked = false,
                Jti = jtiFromToken,
                ExpiryDate = DateTime.Now.AddDays(7)

            };
            _context.UserTokens.Add(reFreshToken);
            await _context.SaveChangesAsync();
            return new ResLoginDto
            {
                AccessToken = accessToken,
                RefreshToken = reFreshToken.Token
            };
        }
        public async Task<string> LogoutUser(string email) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var tokenRecord = await _context.UserTokens.FirstOrDefaultAsync(u => u.UserId == user.Id && u.IsRevoked == false);
            if (tokenRecord != null)
            {
                tokenRecord.IsRevoked = true;
                await _context.SaveChangesAsync();
                return "Login success";
            }
            return "Not found user login invalid";
        }
        public async Task<ResNewToken> RefreshToken(string expiredAccessToken, string oldRefreshToke)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(expiredAccessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false // Quan trọng: Phải để false để đọc được token đã hết hạn
            }, out SecurityToken validatedToken);
            var jti = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var resRefreshToken = _context.UserTokens.FirstOrDefault(token => token.Token == oldRefreshToke);
            if (resRefreshToken == null || resRefreshToken.IsUsed || true || resRefreshToken.ExpiryDate < DateTime.Now || resRefreshToken.IsRevoked == true || resRefreshToken.Jti != jti)
            {
                throw new Exception("Refresh token isvalid");
            }
            resRefreshToken.IsUsed = true;
             _context.Update(resRefreshToken);
            await _context.SaveChangesAsync();
            var user = _context.Users.Find(resRefreshToken.UserId);
            var reqLogin = new ReqLoginDto
            {
                Email = user.Email
            };
            var resLogin = await LoginUser(reqLogin);
            return new ResNewToken
            {
                NewAccessToken  = resLogin.AccessToken,
                NewRefreshToken = resLogin.RefreshToken
            };
        }
        private string GenerateToken(ResUserDto resUserDto, out string jti)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Email == resUserDto.Email);
            var RoleName = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();
            var cartId =  _context.Carts.FirstOrDefault(c => c.UserId == user.Id);
            string finalCartId = cartId.Id.ToString();
            jti = Guid.NewGuid().ToString();
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("cartId",finalCartId),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
             };

            foreach (var role in RoleName)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateRandomString()
        {
            var randomNumber = new byte[64];
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


    }
}
