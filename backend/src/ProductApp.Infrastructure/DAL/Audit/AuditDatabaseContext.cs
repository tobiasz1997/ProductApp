using Microsoft.EntityFrameworkCore;
using ProductApp.Core.AuditLogs.Models;

namespace ProductApp.Infrastructure.DAL.Audit;

public class AuditDatabaseContext: DbContext
{
    public DbSet<AuditLog> AuditLog { get; set; }

    public AuditDatabaseContext(DbContextOptions<AuditDatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditDatabaseContext).Assembly, t => t.Namespace!.Contains("DAL.Audit"));
    }
}