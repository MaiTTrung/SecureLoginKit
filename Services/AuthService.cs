using Microsoft.EntityFrameworkCore;
using SecureLoginKit.Data;
using SecureLoginKit.Models;
using SecureLoginKit.Models.DTOs;
using SecureLoginKit.Services.Interfaces;

namespace SecureLoginKit.Services
{
    public class AuthService :IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _jwtService;

        public AuthService(AppDbContext context, ITokenService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        { 
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username || x.Email == request.Email);
            if (existing != null) {
                return new AuthResponse 
                {
                    Success = false,
                    Message = "Username hoặc Email đã tồn tại!"
                };
            }
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "user"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Success = true,
                Message = "Đăng ký thành công"
            };
                
        }

        public async Task<AuthResponse> FacebookLoginAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Identifer || x.Email == request.Identifer);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Sai tài khoản hoặc mật khẩu!"
                };
            }
            var token = _jwtService.GenerateAccessToken(user);
            return new AuthResponse
            {
                Success = true,
                AccessToken = token,
            };
        }

        
    }
}
