using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.ProductFavourites.Models;
using ProductApp.Core.ProductFavourites.ValueObjects;

namespace ProductApp.Infrastructure.DAL.App.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("app_product");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.Property(x => x.ExternalId)
            .HasConversion(x => x.Value, x => new ExternalId(x));
        builder.Property(x => x.ExternalUrl)
            .HasConversion(x => x.Value, x => new Url(x));
        builder.Property(x => x.PhotoUrl)
            .HasConversion(x => x.Value, x => new Url(x));
        builder.Property(x => x.Title)
            .HasConversion(x => x.Value, x => new Title(x))
            .IsRequired();
        builder.Property(x => x.Price)
            .HasConversion(x => x.Value, x => new Price(x))
            .IsRequired(false);
        builder.Property(x => x.Rating)
            .HasConversion(x => x.Value, x => new Rating(x))
            .IsRequired(false);
    }
}