namespace ca_api.Models
{
public class ClientService:AuditFields
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public DateTime StartedOn { get; set; }
    public bool IsActive { get; set; } = true;

    public List<ClientServiceActivity> Activities { get; set; } = new();
}
}
