using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.Users.Models;

namespace ProductApp.Infrastructure.DAL.Configuration;

public class ProductFavouriteConfiguration : IEntityTypeConfiguration<ProductFavourite>
{
    public void Configure(EntityTypeBuilder<ProductFavourite> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProductId });
        builder.Property(x => x.ProductId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}