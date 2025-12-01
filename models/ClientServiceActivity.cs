namespace ca_api.Models
{
    public class ClientServiceActivity:AuditFields
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClientServiceId { get; set; }
    public ClientService ClientService { get; set; } = null!;

    public Guid ActivityId { get; set; }
    public Activity Activity { get; set; } = null!;

}

}