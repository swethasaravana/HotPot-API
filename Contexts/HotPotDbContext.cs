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
        public DbSet<RestaurantManager> RestaurantManagers { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }



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

            modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Managers)
            .WithOne(m => m.Restaurant)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RestaurantManager>()
            .HasOne(m => m.User)
            .WithOne(u => u.RestaurantManager)
            .HasForeignKey<RestaurantManager>(m => m.Email)
            .HasPrincipalKey<User>(u => u.Username)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeliveryPartner>(entity =>
            {
                entity.HasKey(dp => dp.PartnerId); // Primary Key

                entity.Property(dp => dp.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(dp => dp.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(dp => dp.Phone)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasOne(dp => dp.User)
                      .WithOne()
                      .HasForeignKey<DeliveryPartner>(dp => dp.User)
                      .OnDelete(DeleteBehavior.Cascade); // One-to-one with User
            });


        }
    }
}
