using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApp.Core.AuditLogs.Models;
using ProductApp.Core.Common.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Audit.Configuration;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_audit_log");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Id(x))
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.Action)
            .IsRequired();
        builder.Property(x => x.UserId)
            .IsRequired(false);
        builder.Property(x => x.Metadata)
            .HasColumnType("json")
            .IsRequired(false)
            .HasConversion(x => x == null ? null : JsonSerializer.Serialize(x, (JsonSerializerOptions?)null),
                x => x == null ? null : JsonSerializer.Deserialize<AuditLog>(x, (JsonSerializerOptions?)null));
    }
}