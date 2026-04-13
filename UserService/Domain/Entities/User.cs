
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public UserStatus Status { get; set; } = UserStatus.Active;

    public bool EmailVerified { get; set; } = false;

    public DateTimeOffset? LastLoginAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
}