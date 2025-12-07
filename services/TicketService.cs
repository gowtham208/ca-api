using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ca_api.Data;
using ca_api.Models;

namespace ca_api.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket CreateTicket(CreateTicketDto dto)
        {
            // ✅ Validate referenced entities exist
            var client = _context.Clients.Find(dto.ClientId);
            var service = _context.Services.Find(dto.ServiceId);
            var activity = _context.Activities.Find(dto.ActivityId);
            var user = _context.Users.Find(dto.UserId);

            if (client == null || service == null || activity == null || user == null)
            {
                throw new Exception("Invalid Client / Service / Activity / User reference");
            }

            var ticket = new Ticket
            {
                Name = dto.Name,
                ClientId = dto.ClientId,
                ServiceId = dto.ServiceId,
                ActivityId = dto.ActivityId,
                UserId = dto.UserId,
                Priority = dto.Priority,
                Status = dto.Status,
                DeadLine = dto.DeadLine?.ToUniversalTime()
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket;
        }
public List<TicketResponseDto> GetAllTickets()
{
    return _context.Tickets
        .Include(t => t.Client)      // Include Client navigation
        .Include(t => t.Service)     // Include Service navigation
        .Include(t => t.Activity)    // Include Activity navigation
        .Include(t => t.User)        // Include User navigation
        .Select(t => new TicketResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            CreatedAt = t.CreatedAt, // Assuming AuditFields has CreatedAt
            
            // Client Info
            ClientId = t.ClientId,
            ClientName = t.Client.Name,
            
            // Service Info
            ServiceId = t.ServiceId,
            ServiceName = t.Service.Name,
            
            // Activity Info
            ActivityId = t.ActivityId,
            ActivityName = t.Activity.Name,
            
            // User Info
            UserId = t.UserId,
            UserName = t.User.Name, // or t.User.Name depending on your User model
            
            // Ticket Details
            Priority = t.Priority,
            Status = t.Status,
            Deadline = t.DeadLine
        })
        .ToList();
}

        public TicketResponseDto? GetTicketById(Guid id)
{
    return _context.Tickets
        .Where(t => t.Id == id)
        .Include(t => t.Client)
        .Include(t => t.Service)
        .Include(t => t.Activity)
        .Include(t => t.User)
        .Select(t => new TicketResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            CreatedAt = t.CreatedAt,
            
            // Client Info
            ClientId = t.ClientId,
            ClientName = t.Client.Name,
            
            // Service Info
            ServiceId = t.ServiceId,
            ServiceName = t.Service.Name,
            
            // Activity Info
            ActivityId = t.ActivityId,
            ActivityName = t.Activity.Name,
            
            // User Info
            UserId = t.UserId,
            UserName = t.User.Name, // or t.User.Name
            
            // Ticket Details
            Priority = t.Priority,
            Status = t.Status,
            Deadline = t.DeadLine
        })
        .FirstOrDefault();
}
    }
}
