using SecureLoginKit.Models;

namespace SecureLoginKit.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
