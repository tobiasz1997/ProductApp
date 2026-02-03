using Microsoft.EntityFrameworkCore;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.Users.Models;

namespace ProductApp.Infrastructure.DAL;

public class DatabaseContext: DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductFavourite> ProductFavourite { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}