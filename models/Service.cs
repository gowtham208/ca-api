// Models/Service.cs
namespace ca_api.Models
{
    public class Service:AuditFields
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public ServiceStatus Status { get; set; } = ServiceStatus.Active;
        
    }

    public enum ServiceStatus
    {
        Active,
        Discontinued
    }
}