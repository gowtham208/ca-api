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
        public List<ClientService> ClientServices { get; set; } = new List<ClientService>();
       // public List<ClientServiceActivity> ClientServiceActivities { get; set; } = new List<ClientServiceActivity>();

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

public class ClientResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PinCode { get; set; } = string.Empty; 
    public Guid UserId {get;set;}
    public string UserName {get;set;} = string.Empty;
    public List<ClientServiceInfoDto> Services { get; set; } = new();
}
public class ClientServiceInfoDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public List<ActivityInfoDto> Activities { get; set; } = new();
}
public class ActivityInfoDto
{
    public Guid ActivityId { get; set; }
    public string ActivityName { get; set; } = string.Empty;
}
}