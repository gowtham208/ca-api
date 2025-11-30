using ca_api.Models;

public class ClientServiceMapping: AuditFields
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Foreign keys
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        
       
        
        // Navigation properties
        public virtual Client Client { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        
}  
        
        
          
