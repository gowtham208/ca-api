using ca_api.Models;

namespace ca_api.Interfaces
{
    public interface IClientService
    {
        List<ClientResponseDto> GetAll();
        ClientResponseDto? GetById(Guid id);
        void Create( ClientOnboardingDto dto);
    }
}
