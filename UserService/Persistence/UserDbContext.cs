using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        ApplyAuditing();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditing();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditing()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                SetGuidIdIfNeeded(entry);
                SetCreatedAtIfExists(entry, now);
                SetUpdatedAtIfExists(entry, now);
            }

            if (entry.State == EntityState.Modified)
            {
                SetUpdatedAtIfExists(entry, now);
            }
        }
    }

    private static void SetGuidIdIfNeeded(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        var idProperty = entry.Metadata.FindProperty("Id");
        if (idProperty == null) return;

        var propertyEntry = entry.Property("Id");

        if (propertyEntry.CurrentValue is Guid id && id == Guid.Empty)
        {
            propertyEntry.CurrentValue = Guid.NewGuid();
        }
    }

    private static void SetCreatedAtIfExists(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, DateTimeOffset now)
    {
        var createdAtProperty = entry.Metadata.FindProperty("CreatedAt");
        if (createdAtProperty == null) return;

        entry.Property("CreatedAt").CurrentValue = now;
    }

    private static void SetUpdatedAtIfExists(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, DateTimeOffset now)
    {
        var updatedAtProperty = entry.Metadata.FindProperty("UpdatedAt");
        if (updatedAtProperty == null) return;

        entry.Property("UpdatedAt").CurrentValue = now;
    }
}
