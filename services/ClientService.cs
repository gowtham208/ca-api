using ca_api.Data;
using ca_api.Models;
using ca_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ca_api.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ClientService> _logger;


        public ClientService(ApplicationDbContext db,ILogger<ClientService> logger)
        {
            _db = db;
            _logger = logger;
        }

public List<ClientResponseDto> GetAll()
{
  var clients = _db.Clients
        .Include(c => c.ClientServices)
            .ThenInclude(cs => cs.Service)
        .Include(c => c.ClientServices)
            .ThenInclude(cs => cs.Activities)
                .ThenInclude(csa => csa.Activity).Include(c=>c.User)
       // This splits the query for PostgreSQL
        .ToList();
    return clients.Select(c => new ClientResponseDto
    {
        Id = c.Id,
        Name = c.Name,
        Email = c.Email,
        Phone = c.Phone,
        BusinessType = c.BusinessType,
        Address = c.Address,
        City = c.City,
        State = c.State,
        PinCode = c.PinCode,
        UserId = c.UserId.GetValueOrDefault(),
        UserName= c.User != null ? c.User.Name : string.Empty,
        Services = c.ClientServices.Select(cs => new ClientServiceInfoDto
        {
            ServiceId = cs.ServiceId,
            ServiceName = cs.Service.Name,
            Activities = cs.Activities.Select(a => new ActivityInfoDto
            {
                ActivityId = a.ActivityId,
                ActivityName = a.Activity.Name
            }).ToList()

        }).ToList()

    }).ToList();
}
public ClientResponseDto? GetById(Guid id)
{
    // 1. Eagerly Load all necessary related data using Includes
    var client = _db.Clients
        .Include(c => c.ClientServices)
            .ThenInclude(cs => cs.Service)
        .Include(c => c.ClientServices)
            .ThenInclude(cs => cs.Activities)
                .ThenInclude(csa => csa.Activity).Include(c=>c.User)
        .FirstOrDefault(c => c.Id == id);

    // Check if the client was found
    if (client == null)
    {
        return null;
    }

    // 2. Project/Map the Client entity into a ClientResponseDto
    // This mapping logic is the same as the one used in your GetAll() method.
    var clientResponseDto = new ClientResponseDto
    {
        Id = client.Id,
        Name = client.Name,
        Email = client.Email,
        Phone = client.Phone,
        BusinessType = client.BusinessType,
        Address = client.Address,
        City = client.City,
        State = client.State,
        PinCode = client.PinCode,
        UserId = client.UserId.GetValueOrDefault(),
        UserName= client.User != null ? client.User.Name : string.Empty,

        Services = client.ClientServices.Select(cs => new ClientServiceInfoDto
        {
            ServiceId = cs.ServiceId,
            ServiceName = cs.Service.Name,
            Activities = cs.Activities.Select(a => new ActivityInfoDto
            {
                ActivityId = a.ActivityId,
                ActivityName = a.Activity.Name
            }).ToList()
        }).ToList()
    };

    return clientResponseDto;
}
public void Create(ClientOnboardingDto dto)
{
    using var tx = _db.Database.BeginTransaction();

    try
    {
        // 1️⃣ Create client
        var client = new Client
        {
            Name = dto.Client.Name,
            Email = dto.Client.Email,
            Phone = dto.Client.Phone,
            BusinessType = dto.Client.BusinessType,
            Address = dto.Client.Address,
            City = dto.Client.City,
            State = dto.Client.State,
            PinCode = dto.Client.PinCode,
            UserId = dto.Client.UserId
        };

        _db.Clients.Add(client);
        _db.SaveChanges();

        // 2️⃣ Create service mappings
        foreach (var svc in dto.Services)
        {
            var serviceExists = _db.Services.Any(s => s.Id == svc.ServiceId);
            if (!serviceExists)
                throw new Exception("Invalid Service");

            var clientService = new ca_api.Models.ClientService
            {
                ClientId = client.Id,
                ServiceId = svc.ServiceId,
                StartedOn = DateTime.UtcNow
            };

            _db.ClientServices.Add(clientService);
            _db.SaveChanges();

            // 3️⃣ Create activity mappings
            foreach (var activityId in svc.ActivityIds)
            {
                var validActivity = _db.Activities.Any(a =>
                    a.Id == activityId && a.ServiceId == svc.ServiceId);

                if (!validActivity)
                    throw new Exception("Invalid Activity");

                _db.ClientServiceActivities.Add(
                    new ClientServiceActivity
                    {
                        ClientServiceId = clientService.Id,
                        ActivityId = activityId,
                        //SubscribedOn = DateTime.UtcNow
                    });
            }
        }

        _db.SaveChanges();
        tx.Commit();
    }
    catch
    {
        tx.Rollback();
        throw;
    }
}

    }
}


 // select * from Clients c left join ClientServices cs on c.Id=cs.ClientId 
 // left join Services s on cs.ServiceId=s.Id left join ClientServiceActivities csa on cs.Id=csa.ClientServiceId
 // left join Activities a on csa.ActivityId=a.Id;