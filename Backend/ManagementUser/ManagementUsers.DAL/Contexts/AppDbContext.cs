using Microsoft.EntityFrameworkCore;
using ManagementUsers.DAL.Mappings;
using ManagementUsers.DAL.Entities;


namespace ManagementUsers.DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<DependentModel> Dependents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new DependentMapping());
        }
    }
}
