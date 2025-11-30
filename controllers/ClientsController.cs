// // Controllers/ClientsController.cs
// using Microsoft.AspNetCore.Mvc;
// using ca_api.Models;
// using ca_api.Services;

// namespace ca_api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ClientsController : ControllerBase
//     {
//         private readonly ClientService _clientService;

//         public ClientsController(ClientService clientService)
//         {
//             _clientService = clientService;
//         }

//         [HttpGet]
//         public ActionResult<List<Client>> GetAll()
//         {
//             try
//             {
//                 Console.WriteLine("inside get all");
//                 var clients = _clientService.GetAll();
//                 return Ok(clients);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Internal server error: {ex.Message}");
//             }
//         }

//         [HttpGet("{id}")]
//         public ActionResult<Client> GetById(string id)
//         {
//             try
//             {
//                 var client = _clientService.GetById(id);
//                 if (client == null)
//                 {
//                     return NotFound($"Client with ID {id} not found");
//                 }
//                 return Ok(client);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Internal server error: {ex.Message}");
//             }
//         }

//         [HttpPost]
//         public ActionResult<Client> Create([FromBody] Client clientData)
//         {
//             try
//             {
//                 var client = _clientService.Create(clientData);
//                 return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
//             }
//             catch (Exception ex)
//             {
//                 return BadRequest($"Error creating client: {ex.Message}");
//             }
//         }

//         [HttpPut("{id}")]
//         public ActionResult<Client> Update(string id, [FromBody] Client clientData)
//         {
//             try
//             {
//                 var client = _clientService.Update(id, clientData);
//                 return Ok(client);
//             }
//             catch (Exception ex)
//             {
//                 if (ex.Message.Contains("not found"))
//                 {
//                     return NotFound(ex.Message);
//                 }
//                 return BadRequest($"Error updating client: {ex.Message}");
//             }
//         }

//         [HttpDelete("{id}")]
//         public IActionResult Delete(string id)
//         {
//             try
//             {
//                 _clientService.Delete(id);
//                 return NoContent();
//             }
//             catch (Exception ex)
//             {
//                 if (ex.Message.Contains("not found"))
//                 {
//                     return NotFound(ex.Message);
//                 }
//                 return BadRequest($"Error deleting client: {ex.Message}");
//             }
//         }

//         [HttpGet("search")]
//         public ActionResult<List<Client>> Search([FromQuery] string query)
//         {
//             try
//             {
//                 var clients = _clientService.Search(query);
//                 return Ok(clients);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Internal server error: {ex.Message}");
//             }
//         }
//     }
// }