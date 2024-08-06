using System.Reflection.Metadata;
using chatmobile.entites;
using Microsoft.EntityFrameworkCore;
namespace chatmobile.DB
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Test> Test { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserModalBuilder.CreateUserModalBuilder(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

    }
}