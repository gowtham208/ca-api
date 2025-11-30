using ca_api.Models;
using ca_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ca_api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/clients
        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var client = _clientService.GetById(id);

            if (client == null)
                return NotFound("Client not found");

            return Ok(client);
        }

        // POST: api/clients
        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            var client = _clientService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }
    }
}
