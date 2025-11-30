// Services/UserManager.cs
using ca_api.Data;
using ca_api.Interfaces;
using ca_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ca_api.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Guid CreateUser(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _logger.LogInformation("User created. UserId: {UserId}", user.Id);

            return user.Id;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User GetUserById(Guid id)
        {
            var user = _context.Users
                               .AsNoTracking()
                               .FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user;
        }
    }
}
