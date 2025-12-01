using ca_api.Models;

namespace ca_api.Interfaces
{
    public interface IClientService
    {
        List<Client> GetAll();
        Client? GetById(Guid id);
        void Create( ClientOnboardingDto dto);
    }
}
