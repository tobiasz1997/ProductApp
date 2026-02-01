using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.HasIndex(x => x.Token).IsUnique();
        builder.Property(x => x.Token)
            .HasConversion(x => x.Value, x => new Token(x))
            .IsRequired();
        builder.HasIndex(x => x.Token).IsUnique();
        builder.Property(x => x.ExpiresAt).IsRequired();
    }
}