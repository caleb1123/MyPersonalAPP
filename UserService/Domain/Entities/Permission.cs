namespace UserService.Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? GroupName { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}