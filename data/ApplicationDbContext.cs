// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using ca_api.Models;

namespace ca_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ClientServiceMapping> ClientServiceMappings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(c => c.User)
          .WithMany(u => u.Clients)
          .HasForeignKey(c => c.UserId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.SetNull);

            });
            // Service configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                // Configure one-to-many relationship with Activities
                entity.HasMany(s => s.Activities)
                      .WithOne(a => a.Service)
                      .HasForeignKey(a => a.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade);
                // Activity configuration
                modelBuilder.Entity<Activity>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Frequency).HasConversion<string>();
                    entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                    entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                    // Relationship with Service
                    entity.HasOne(a => a.Service)
                          .WithMany(s => s.Activities)
                          .HasForeignKey(a => a.ServiceId)
                          .OnDelete(DeleteBehavior.Cascade);
                });
            });
            modelBuilder.Entity<Ticket>(entity =>
    {
        entity.HasKey(t => t.Id);

        // Client → Tasks (1 : many)
        entity.HasOne(t => t.Client)
              .WithMany(c => c.Tickets)
              .HasForeignKey(t => t.ClientId)
              .OnDelete(DeleteBehavior.Restrict);

        // Service → Tasks (1 : many)
        entity.HasOne(t => t.Service)
              .WithMany(s => s.Tickets)
              .HasForeignKey(t => t.ServiceId)
              .OnDelete(DeleteBehavior.Restrict);

        // Activity → Tasks (1 : many)
        entity.HasOne(t => t.Activity)
              .WithMany(a => a.Tickets)
              .HasForeignKey(t => t.ActivityId)
              .OnDelete(DeleteBehavior.Restrict);

        // User → Tasks (1 : many)
        entity.HasOne(t => t.User)
              .WithMany(u => u.Tickets)
              .HasForeignKey(t => t.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.Property(t => t.Name)
              .IsRequired()
              .HasMaxLength(200);

        entity.Property(t => t.Priority)
              .IsRequired();

        entity.Property(t => t.Status)
              .IsRequired();
    });
        modelBuilder.Entity<User>(entity =>
{
    // ✅ Primary key
    entity.HasKey(u => u.Id);

    // ✅ Properties configuration
    entity.Property(u => u.Name)
          .IsRequired()
          .HasMaxLength(150);

    entity.Property(u => u.Email)
          .IsRequired()
          .HasMaxLength(200);

    entity.Property(u => u.Role)
          .HasConversion<string>()   // Enum stored as text
          .IsRequired();

    // ✅ User → Tickets (1 : many, required on Ticket)
    entity.HasMany(u => u.Tickets)
          .WithOne(t => t.User)
          .HasForeignKey(t => t.UserId)
          .OnDelete(DeleteBehavior.Restrict);

    // ✅ User → Clients (1 : many, optional on Client)
    entity.HasMany(u => u.Clients)
          .WithOne(c => c.User)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.SetNull);
});
        }
    }
}