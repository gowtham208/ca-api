// Models/Activity.cs
namespace ca_api.Models
{
    public class Ticket:AuditFields
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!; // Navigation property
        public Guid ServiceId { get; set; }
        public Service Service{ get; set; }=null!; // Navigation property

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }=null!; // Navigation property

        public Guid UserId { get; set; }
         public User User { get; set; } = null!; // Navigation property

        public Priority Priority { get; set; } = Priority.Medium;
        public Status Status { get; set; } = Status.Pending;
         
        public DateTime? DeadLine { get; set; }
        
        
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public class CreateTicketDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ActivityId { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public Status Status { get; set; } = Status.Pending;
        public DateTime? DeadLine { get; set; }

        public Guid UserId { get; set;}
    }

  public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // Client Info
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        
        // Service Info
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        
        // Activity Info
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        
        // User Info (Assigned to)
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        
        // Ticket Details
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime? Deadline { get; set; }
    }


}