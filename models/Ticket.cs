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
    }


}