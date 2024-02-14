using Microsoft.EntityFrameworkCore;
using simple_Web.Domain.Entities;

namespace simple_Web.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new SuperAdminConfiguration());
        }
    }
}
