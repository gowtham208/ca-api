using ca_api.Models;

namespace ca_api.Interfaces
{
    public interface IClientService
    {
        List<Client> GetAll();
        Client? GetById(Guid id);
        Client Create(CreateClientDto dto);
    }
}
