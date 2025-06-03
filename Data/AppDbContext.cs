using Microsoft.EntityFrameworkCore;
using SecureLoginKit.Models;

namespace SecureLoginKit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<ExternalLogin> ExternalLogins { get; set; }

        protected AppDbContext()
        {
        }
    }
}
