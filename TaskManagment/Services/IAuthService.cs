using TaskManagment.Models;

namespace TaskManagment.Services
{
    public interface IAuthService
    {
        public Task<User?> Register(UserDTO user);
        public string? Login(UserDTO user);

    }
}
