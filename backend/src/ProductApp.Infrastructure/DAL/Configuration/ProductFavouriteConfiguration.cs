using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Favourites.Models;
using ProductApp.Core.Favourites.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Configuration;

public class ProductFavouriteConfiguration : IEntityTypeConfiguration<ProductFavourite>
{
    public void Configure(EntityTypeBuilder<ProductFavourite> builder)
    {
        builder.HasKey(x => new { x.UserId, x.ProductId });
        builder.Property(x => x.ProductId)
            .HasConversion(x => x.Value, x => new ProductId(x))
            .IsRequired();
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
    }
}