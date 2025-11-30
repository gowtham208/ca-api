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

        public Client Create(CreateClientDto dto)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                BusinessType = dto.BusinessType,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                PinCode = dto.PinCode
                // UserId intentionally NOT set (optional relationship)
            };

            _db.Clients.Add(client);
            _db.SaveChanges();
         _logger.LogInformation("client created. clientId: {ClientId}", client.Id);


            return client;
        }
    }
}
