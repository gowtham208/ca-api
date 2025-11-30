using ca_api.Models;
using ca_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ca_api.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public IActionResult Create(CreateTicketDto dto)
        {
            try
            {
                var ticket = _ticketService.CreateTicket(dto);
                return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ticketService.GetAllTickets());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var ticket = _ticketService.GetTicketById(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            return Ok(ticket);
        }
    }
}
