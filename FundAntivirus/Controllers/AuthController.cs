using Microsoft.AspNetCore.Mvc;
using FundAntivirus.Data;
using FundAntivirus.Models;
using FundAntivirus.Services;
using System.Security.Cryptography;
using System.Text;
namespace FundAntivirus.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;
        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            user.Password = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
            _context.User.Add(user);
            _context.SaveChanges();
            return Ok(new {message = "User registered successfully"});
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var userInDb = _context.User.FirstOrDefault(u => u.Name == user.Name && u.Password == Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password))));
            if(userInDb == null)
            {
                return Unauthorized(new {message = "Invalid username or password"});
            }
            return Ok(new {token = _authService.GenerateToken(userInDb)});
        }
    }
}