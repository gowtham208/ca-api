using ca_api.Models; 

namespace ca_api.interfaces
{
public interface IServiceService
{
    Guid CreateService(CreateServiceDto dto);
    List<Service> GetAllServices();
    Service GetServiceById(Guid id);
}
}
