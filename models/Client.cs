// Models/Client.cs
namespace ca_api.Models
{
    public class Client:AuditFields

    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string BusinessType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;

       // public List<Service>? Services { get; set; } = new List<Service>();
        //public List<ClientServiceMapping>? clientService{ get; set; } = new List<ClientServiceMapping>();
        public List<Ticket>? Tickets { get; set; } = new List<Ticket>();

        public User? User {get;set;}

        public Guid? UserId {get;set;}

    }


    public class CreateClientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string BusinessType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty; 

        public Guid? UserId {get;set;}

    }
public class ClientOnboardingDto
{
    public CreateClientDto Client { get; set; } = null!;
    public List<ClientServiceSubscriptionDto> Services { get; set; } = new();
}
public class ClientServiceSubscriptionDto
{
    public Guid ServiceId { get; set; }
    public List<Guid> ActivityIds { get; set; } = new();
}
}