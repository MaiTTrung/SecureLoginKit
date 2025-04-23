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

        protected AppDbContext()
        {
        }
    }
}
