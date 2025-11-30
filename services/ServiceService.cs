using ca_api.Models;
using ca_api.Data;
using ca_api.interfaces;
using Microsoft.EntityFrameworkCore;
namespace ca_api.Services
{
public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ServiceService> _logger;

    public ServiceService(ApplicationDbContext context, ILogger<ServiceService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Guid CreateService(CreateServiceDto dto)
    {
        try
        {
            var service = new Service
            {
                Name = dto.Name,
                Status = dto.Status,
                Activities = dto.Activities.Select(a => new Activity
{
    Name = a.Name,
    Frequency = a.Frequency,
    Amount = a.Amount,
    FinancialYear = a.FinancialYear,
    Deadline = a.Deadline.HasValue 
        ? DateTime.SpecifyKind(a.Deadline.Value, DateTimeKind.Utc) 
        : null
}).ToList()
            };

            _context.Services.Add(service);
            _context.SaveChanges();

            _logger.LogInformation("Service created successfully. ServiceId: {ServiceId}", service.Id);

            return service.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating service");
            throw new ApplicationException("Unable to create service");
        }
    }

    public List<Service> GetAllServices()
    {
        try
        {
            return _context.Services
                .Include(s => s.Activities)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching services");
            throw new ApplicationException("Unable to fetch services");
        }
    }

    public Service GetServiceById(Guid id)
    {
        try
        {
            var service = _context.Services
                .Include(s => s.Activities)
                .FirstOrDefault(s => s.Id == id);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            return service;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching service with ID {ServiceId}", id);
            throw;
        }
    }
}
}
