using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureLoginKit.Models
{
    public class ExternalLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Provider { get; set; }
        [Required]
        public string ProviderId {  get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
