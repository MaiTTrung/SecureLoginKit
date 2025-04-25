using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SecureLoginKit.Models;
using SecureLoginKit.Services.Interfaces;
using SecureLoginKit.Models.Configurations;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace SecureLoginKit.Services
{
    public class JwtService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtService( IOptions<JwtSettings> options )
        {
            _jwtSettings = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExp),
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() {
            return "";
        }

        
    }
}
