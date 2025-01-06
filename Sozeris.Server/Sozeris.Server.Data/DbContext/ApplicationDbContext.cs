using Microsoft.EntityFrameworkCore;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Data.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
}