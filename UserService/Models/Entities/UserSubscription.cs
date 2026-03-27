namespace UserService.Models.Entities
{
    public class UserSubscription
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "active";
        public bool AutoRenew { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public Plan Plan { get; set; } = null!;
    }
}
