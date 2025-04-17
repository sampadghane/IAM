using AccessManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Data

{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) { }


        public DbSet<User> user { get; set; }

        public DbSet<AuditLog> logs { get; set; }

        public DbSet<UserEmail> userEmail { get; set; }
    }

  
    
}
