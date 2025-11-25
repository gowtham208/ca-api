// Services/ClientService.cs
using ca_api.Data;
using ca_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ca_api.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        // GET all clients
        public List<Client> GetAll()
        {
            Console.WriteLine("inside get all service");
            return _context.Clients.ToList();
        }

        // GET client by ID
        public Client GetById(string id)
        {
            var client = _context.Clients.Find(id);
            return client;
        }

        // CREATE new client
        public Client Create(Client clientData)
        {
            Thread.Sleep(500);
            var newClient = new Client
            {
                Id = Guid.NewGuid().ToString(),
                Name = clientData.Name,
                Email = clientData.Email,
                Phone = clientData.Phone,
                BusinessType = clientData.BusinessType,
                Address = clientData.Address,
                City = clientData.City,
                State = clientData.State,
                PinCode = clientData.PinCode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Clients.Add(newClient);
            _context.SaveChanges();
            
            Console.WriteLine($"Creating client: {newClient.Name}");
            return newClient;
        }

        // UPDATE client
        public Client Update(string id, Client clientData)
        {
            Thread.Sleep(500);
            var existingClient = _context.Clients.Find(id);
            
            if (existingClient == null)
            {
                throw new Exception($"Client with id {id} not found");
            }

            existingClient.Name = clientData.Name;
            existingClient.Email = clientData.Email;
            existingClient.Phone = clientData.Phone;
            existingClient.BusinessType = clientData.BusinessType;
            existingClient.Address = clientData.Address;
            existingClient.City = clientData.City;
            existingClient.State = clientData.State;
            existingClient.PinCode = clientData.PinCode;
            existingClient.UpdatedAt = DateTime.UtcNow;

            _context.Clients.Update(existingClient);
            _context.SaveChanges();

            Console.WriteLine($"Updating client: {existingClient.Name}");
            return existingClient;
        }

        // DELETE client
        public void Delete(string id)
        {
            Thread.Sleep(300);
            var client = _context.Clients.Find(id);
            
            if (client == null)
            {
                throw new Exception($"Client with id {id} not found");
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            Console.WriteLine($"Deleting client: {client.Name}");
        }

        // SEARCH clients
        public List<Client> Search(string query)
        {
            Thread.Sleep(400);
            var lowerQuery = query.ToLower();
            
            return _context.Clients
                .Where(client =>
                    client.Name.ToLower().Contains(lowerQuery) ||
                    client.Email.ToLower().Contains(lowerQuery) ||
                    client.BusinessType.ToLower().Contains(lowerQuery))
                .ToList();
        }
    }
}