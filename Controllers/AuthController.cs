using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureLoginKit.Data;
using SecureLoginKit.Models;
using SecureLoginKit.Models.DTOs;
using SecureLoginKit.Services;
using SecureLoginKit.Services.Interfaces;

namespace SecureLoginKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private ITokenService _jwtService;
        public AuthController(AppDbContext context, ITokenService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterRequest request)
        {
            var exitUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);
            if (exitUser != null)
            {
                return BadRequest("Username hoặc Email đã tồn tại");
            }

            // create new user
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "user",
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();
            return Ok("Đăng ký thành công.");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var exitUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Identifer || u.Email == request.Identifer);
            if (exitUser == null)
            {
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác");
            }
            if (exitUser != null && !BCrypt.Net.BCrypt.Verify(request.Password, exitUser.PasswordHash))
            {
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác");
            }

            var accessToken = _jwtService.GenerateAccessToken(exitUser);
            return Ok(
                new { AccessToken = accessToken }
            );
        }
    }
}
