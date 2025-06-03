namespace SecureLoginKit.Models.DTOs
{
    public class AuthResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public string? AccessToken { get; set; }
    }
}
