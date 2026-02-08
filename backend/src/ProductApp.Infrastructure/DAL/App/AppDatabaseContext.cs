using Microsoft.EntityFrameworkCore;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.Users.Models;

namespace ProductApp.Infrastructure.DAL.App;

public class AppDatabaseContext: DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductFavourite> ProductFavourite { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly, t => t.Namespace!.Contains("DAL.App"));
    }
}