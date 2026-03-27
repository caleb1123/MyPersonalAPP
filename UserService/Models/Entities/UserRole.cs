namespace UserService.Models.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime AssignedAt { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
