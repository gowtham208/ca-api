using ca_api.Data;
using ca_api.Models;
using ca_api.Interfaces;

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

        public List<Client> GetAll()
        {
            return _db.Clients.ToList();
        }

        public Client? GetById(Guid id)
        {
            return _db.Clients.FirstOrDefault(c => c.Id == id);
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
