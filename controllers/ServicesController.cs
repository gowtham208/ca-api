using Microsoft.AspNetCore.Mvc;
using ca_api.Models;
using ca_api.interfaces;


[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(IServiceService serviceService, ILogger<ServicesController> logger)
    {
        _serviceService = serviceService;
        _logger = logger;
    }

    // ✅ Create Service + Activities
    [HttpPost]
    public IActionResult CreateService([FromBody] CreateServiceDto dto)
    {
        try
        {
            var serviceId = _serviceService.CreateService(dto);
            return CreatedAtAction(nameof(GetServiceById), new { id = serviceId }, null);
        }
        catch (ApplicationException ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error");
            return StatusCode(500, "Internal server error");
        }
    }

    // ✅ Get all services
    [HttpGet]
    public IActionResult GetAll()
    {
        var services = _serviceService.GetAllServices();
        return Ok(services);
    }

    // ✅ Get service by id
    [HttpGet("{id}")]
    public IActionResult GetServiceById(Guid id)
    {
        try
        {
            var service = _serviceService.GetServiceById(id);
            return Ok(service);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
