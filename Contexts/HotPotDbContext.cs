using HotPotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotPotAPI.Contexts
{
    public class HotPotDbContext : DbContext
    {
        public HotPotDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Admin>()
            .HasOne(a => a.User)
            .WithOne(u => u.Admin)
            .HasForeignKey<Admin>(a => a.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
