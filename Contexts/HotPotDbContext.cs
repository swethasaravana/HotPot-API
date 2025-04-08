using HotPotAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantManager> RestaurantManagers { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer relationship
            modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .HasForeignKey<Customer>(c => c.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

            // Admin relationship
            modelBuilder.Entity<Admin>()
            .HasOne(a => a.User)
            .WithOne(u => u.Admin)
            .HasForeignKey<Admin>(a => a.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

            // RestaurantManager relationship
            modelBuilder.Entity<RestaurantManager>()
            .HasOne(rm => rm.User)
            .WithOne(u => u.RestaurantManager)
            .HasForeignKey<RestaurantManager>(rm => rm.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

            // DeliveryPartner relationship
            modelBuilder.Entity<DeliveryPartner>()
            .HasOne(dp => dp.User)
            .WithOne(u => u.DeliveryPartner)
            .HasForeignKey<DeliveryPartner>(dp => dp.Username)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
