using AutoMapper;
using ChatApp.Core.Dtos;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ChatApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserById(string userId);

        Task<ApplicationUser> GetUserByEmail(string email);

        Task<ApplicationUser> GetUser(string token);

        Task<List<UserVM>> GetUsers(string value);

        Task<List<UserVM>> GetUsersByIds(IEnumerable<string> userIds);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> GetUser(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (string.IsNullOrWhiteSpace(email)) return null;

            return await GetUserByEmail(email);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return result;
        }

        public async Task<List<UserVM>> GetUsers(string value)
        {
            var query = await _unitOfWork.UserRepository
                .FindAsync(x => x.Email == value || (!string.IsNullOrEmpty(x.FullName) && x.FullName.Contains(value)));

            return query.Any() ? _mapper.Map<List<UserVM>>(query) : new List<UserVM>();
        }

        public async Task<List<UserVM>> GetUsersByIds(IEnumerable<string> userIds)
        {
            if (userIds is null || !userIds.Any()) return new List<UserVM>();
            var query = await _unitOfWork.UserRepository.FindAsync(x => userIds.Contains(x.Id));
            return _mapper.Map<List<UserVM>>(query);
        }
    }
}