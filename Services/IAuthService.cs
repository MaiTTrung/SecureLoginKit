using SecureLoginKit.Models.DTOs;

namespace SecureLoginKit.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest);

        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);

        Task<AuthResponse> FacebookLoginAsync();
    }
}
