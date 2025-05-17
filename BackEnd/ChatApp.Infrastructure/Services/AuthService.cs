using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastructure.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest);

        Task<string> LoginUserAsync(string email, string password);

        Task<bool> LogoutAsync(string token);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRedisService _redisService;
        private readonly IConfiguration _configuration;
        public AuthService(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRedisService redisService,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _redisService = redisService;
            _configuration = configuration;
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded) return null;

            var user = await _unitOfWork.UserRepository.FindAsync(u => u.Email.Equals(email));
            if (user is null) return null;

            return GenerateJwtToken(user.FirstOrDefault());
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecrectKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FullName", user.FullName),
                new Claim("Avatar", user.Avartar),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.
                    Add(TimeSpan.Parse(_configuration["Jwt:TokenLifetime"])),
                SigningCredentials = credentials
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> LogoutAsync(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
            if (jwtToken == null) return false;

            if (expClaim != null && long.TryParse(expClaim, out long expSeconds))
            {
                var expiration = DateTimeOffset.FromUnixTimeSeconds(expSeconds) - DateTimeOffset.UtcNow;
                await _redisService.SetCacheAsync($"blacklist:{token}", "revoked", expiration);
            }
            return true;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest registerRequest )
        {
            var user = new ApplicationUser()
            {
                Email = registerRequest.Email,
                FullName = registerRequest.FullName,
                Avartar = registerRequest.Avartar,
                Gender = registerRequest.Gender,
                UserName = registerRequest.Email
            };
            var checkExist = await _unitOfWork.UserRepository.FindAsync(u => u.UserName.ToLower().Equals(user.UserName.ToLower()) && u.Email.Equals(user.Email));
            if (checkExist.Any())
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            }
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
                await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}