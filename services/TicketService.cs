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

        public List<Ticket> GetAllTickets()
        {
            return _context.Tickets.ToList();
        }

        public Ticket? GetTicketById(Guid id)
        {
            return _context.Tickets.FirstOrDefault(t => t.Id == id);
        }
    }
}
