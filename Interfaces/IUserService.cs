// Interfaces/IUserManager.cs
using ca_api.Models;

namespace ca_api.Interfaces
{
    public interface IUserService
    {
        Guid CreateUser(CreateUserDto dto);
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
    }
}
