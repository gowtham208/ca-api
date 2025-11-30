using ca_api.Models;
namespace ca_api.Services
{
    public interface ITicketService
    {
        Ticket CreateTicket(CreateTicketDto dto);
        List<Ticket> GetAllTickets();
        Ticket? GetTicketById(Guid id);
    }
}
