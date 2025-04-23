using System.ComponentModel.DataAnnotations;

namespace SecureLoginKit.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Identifer { get; set; }  // email or username

        [Required]
        public string Password { get; set; }
    }
}
