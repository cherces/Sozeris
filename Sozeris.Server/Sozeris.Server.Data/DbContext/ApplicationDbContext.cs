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
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();

            entity.Property(u => u.Login).IsRequired().HasMaxLength(255);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
            entity.Property(u => u.Salt).IsRequired();
            entity.Property(u => u.Phone).HasMaxLength(50);
            entity.Property(u => u.Address).HasMaxLength(500);

            entity.HasMany(u => u.JwtRefreshTokens)
                  .WithOne(t => t.User)
                  .HasForeignKey(t => t.UserId);
        });

        modelBuilder.Entity<JwtRefreshToken>(entity =>
        {
            entity.ToTable("JwtRefreshTokens");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Token).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.Name).IsRequired().HasMaxLength(255);
            entity.Property(p => p.Price).HasColumnType("numeric(10,2)");
            entity.Property(p => p.Image).IsRequired();
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.ToTable("Subscriptions");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).ValueGeneratedOnAdd();

            entity.Property(s => s.StartDate).IsRequired();
            entity.Property(s => s.EndDate).IsRequired();
            entity.Property(s => s.IsActive).IsRequired();

            entity.HasOne(s => s.User)
                  .WithMany()
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).ValueGeneratedOnAdd();

            entity.Property(o => o.Quantity).IsRequired();
            entity.Property(o => o.Price).HasColumnType("numeric(10,2)");

            entity.HasOne(o => o.Subscription)
                  .WithMany(s => s.Orders)
                  .HasForeignKey(o => o.SubscriptionId);

            entity.HasOne(o => o.Product)
                  .WithMany()
                  .HasForeignKey(o => o.ProductId);
        });

        modelBuilder.Entity<DeliveryHistory>(entity =>
        {
            entity.ToTable("DeliveryHistory");

            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).ValueGeneratedOnAdd();

            entity.Property(d => d.Status).IsRequired();
            entity.Property(d => d.Reason).HasMaxLength(500);

            entity.HasOne(d => d.Subscription)
                  .WithMany()
                  .HasForeignKey(d => d.SubscriptionId);

            entity.HasOne(d => d.Courier)
                  .WithMany()
                  .HasForeignKey(d => d.UserId);
        });
    }
}