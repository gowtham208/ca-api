namespace ca_api.Models
{
    public class User:AuditFields
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Standard;

        public List<Ticket>? Tickets { get; set; } = new List<Ticket>();    

        public List<Client>? Clients { get; set; } = new List<Client>();   
        
    }

    public enum UserRole
    {
        Admin,
        Standard,
        Guest
    }

    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Standard;

    }

    public class AssignTicketsToUserDto
{
    public Guid UserId { get; set; }
    public List<Guid> TicketIds { get; set; } = new();
}

public class AssignClientsToUserDto
{
    public Guid UserId { get; set; }
    public List<Guid> ClientIds { get; set; } = new();
}

    
}