using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Domain.Entities;

namespace Sozeris.Server.Data.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<DeliveryHistory> DeliveryHistory { get; set; }
    public DbSet<JwtRefreshToken> JwtRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<DeliveryHistory>().ToTable("DeliveryHistory");
        modelBuilder.Entity<JwtRefreshToken>().ToTable("JwtRefreshTokens");
    }
}