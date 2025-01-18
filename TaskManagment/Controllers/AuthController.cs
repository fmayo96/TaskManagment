using Microsoft.AspNetCore.Mvc;
using TaskManagment.Models;
using TaskManagment.Services;

namespace TaskManagment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(UserDTO user)
        {
            var newUser = await _authService.Register(user);
            return (newUser is null)? BadRequest("Username already exists") : Ok(newUser);
        }
        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO user)
        {
            var token = _authService.Login(user);
            return (token is null) ? BadRequest("Invalid Username or Password.") : Ok(token);
        }
    }
}
