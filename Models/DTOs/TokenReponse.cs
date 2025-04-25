namespace SecureLoginKit.Models.DTOs
{
    public class TokenReponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
