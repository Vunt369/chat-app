using ChatApp.Attributes;
using ChatApp.Core.Dtos;
using ChatApp.Helper;
using ChatApp.Infrastructure.RequestModels;
using ChatApp.Infrastructure.Response;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(model);
            if (result.Succeeded)
                return Created();

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginUserAsync(model.Email, model.Password);
            if (result != null)
               return Ok(ApiResponseFactory.Success(result));

            return NotFound(ApiResponseFactory.Fail<UserDTO>("Email or Password is not found"));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return Unauthorized(ApiResponseFactory.Fail<string>("Token is required", 401));

            var result = await _authService.LogoutAsync(token);
            if (result)
                return Ok(ApiResponseFactory.Success<string>(string.Empty));

            return BadRequest("Logout failed.");
        }

        [HttpPost]
        [BlacklistedToken]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return BadRequest("Token is required");

            var result = await _userService.GetUser(token);

            if(result != null)
            return Ok(ApiResponseFactory.Success(result));

            return Forbid();
        }
    }
}