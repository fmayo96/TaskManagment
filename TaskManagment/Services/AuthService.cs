using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagment.Models;

namespace TaskManagment.Services
{
    public class AuthService : IAuthService
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly IConfiguration _configuration;
        public AuthService(TodoDbContext todoDbContext, IConfiguration configuration) {
            _todoDbContext = todoDbContext;
            _configuration = configuration;
        }
        public async Task<User?> Register(UserDTO user)
        {
            var checkUser = _todoDbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (checkUser is not null) return null;
            var newUser = new User()
            {
                UserName = user.UserName,
            };
            var passwordHash = new PasswordHasher<User>().HashPassword(newUser, user.Password);
            newUser.PasswordHash = passwordHash;
            await _todoDbContext.AddAsync(newUser);
            await _todoDbContext.SaveChangesAsync();
            return newUser;
        }
        public string? Login(UserDTO user)
        {
            var login = _todoDbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (login is null) return null;
            if (new PasswordHasher<User>()
                .VerifyHashedPassword(login, login.PasswordHash, user.Password) == PasswordVerificationResult.Failed) return null;
            string token = CreateJWT(login);
            return token;
        }
        private string CreateJWT(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("Issuer"),
                    audience: _configuration.GetValue<string>("Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
