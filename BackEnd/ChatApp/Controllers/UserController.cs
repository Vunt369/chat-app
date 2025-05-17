using AutoMapper;
using ChatApp.Core.Dtos;
using ChatApp.Core.Models;
using ChatApp.Helper;
using ChatApp.Infrastructure.Response;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRedisService _redisService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IRedisService redisService, IMapper mapper)
        {
            _userService = userService;
            _redisService = redisService;
            _mapper = mapper;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            if(result != null) return Ok(result);
            return NotFound($"User with email {email} not found!");
        }
        [HttpPost]
        [Route("search/{value}")]
        public async Task<IActionResult> SearchUser(string value)
        {
            var result = await _userService.GetUsers(value);
            if (result != null) return Ok(ApiResponseFactory.Success(result));
            return NotFound($"User with {value} not found!");
        }
        [HttpGet]
        [Route("online-users")]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var userIds = await _redisService.GetSetAsync("user_connections");
            if (userIds == null) return NotFound();

            var users = await _userService.GetUsersByIds(userIds);
            return Ok(users);
        }
    }
}
