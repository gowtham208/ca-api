// Models/Activity.cs
namespace ca_api.Models
{
    public class Activity:AuditFields
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ServiceId { get; set; } = string.Empty;
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
}