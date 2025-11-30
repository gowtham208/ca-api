// Models/Service.cs
namespace ca_api.Models
{
    public class Service:AuditFields
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ServiceStatus Status { get; set; } = ServiceStatus.Active;

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        
    }

    public enum ServiceStatus
    {
        Active,
        Discontinued
    }
public class CreateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public ServiceStatus Status { get; set; } = ServiceStatus.Active;

    public List<CreateActivityDto> Activities { get; set; }
        = new();
}

}

//     public class ServiceSelectionDTO
//     {
//         public Guid SelectedServiceID{ get; set; }
//         public List<Guid> SelectedActivityIds { get; set; } = new List<Guid>();


//     }
// }