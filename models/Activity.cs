// Models/Activity.cs
namespace ca_api.Models
{
    public class Activity:AuditFields
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid ServiceId { get; set; }
        public Service Service { get; set; } = null!; // Navigation property
        public Frequency? Frequency { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Deadline { get; set; }
        public string? FinancialYear { get; set; }
        
        
    }

    public enum Frequency
    {
        Monthly,
        Quarterly,
        Yearly
    }

    public class CreateActivityDto
    {
        public string Name { get; set; } = string.Empty;
        public Frequency? Frequency { get; set; }
        public decimal Amount { get; set; }
        public string? FinancialYear { get; set; }
        public DateTime? Deadline { get; set; }
    }


}