using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManagementAPI.Data;
using UsersManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace UsersManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApplicationDbContext context, JwtHelper jwtHelper, ILogger<AuthController> logger)
        {
            _context = context;
            _jwtHelper = jwtHelper;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.email == loginDto.Email);
                if (user == null || _passwordHasher.VerifyHashedPassword(user, user.password, loginDto.Password) == PasswordVerificationResult.Failed)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = _jwtHelper.GenerateToken(user.email);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

    }


    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
