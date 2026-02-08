using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.App.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("app_user");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x));
        builder.HasIndex(x => x.Login).IsUnique();
        builder.Property(x => x.Login)
            .HasConversion(x => x.Value, x => new Login(x))
            .IsRequired();
        builder.Property(x => x.PasswordHash)
            .HasConversion(x => x.Value, x => new PasswordHash(x))
            .IsRequired();
    }
}