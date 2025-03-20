using FundacionAntivirus.Data;
using FundacionAntivirus.Dtos;
using FundacionAntivirus.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;

        public AuthController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        /// <summary>
        /// Inicia sesión con credenciales de usuario.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous] // Permite acceso sin autenticación
        public async Task<ActionResult<UserResponseDto>> LoginAsync([FromBody] UserRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userResponse = await _authService.LoginAsync(dto);
            if (userResponse == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _authService.GenerateJwt(userResponse);
            return Ok(new
            {
                message = "Login successful",
                Token = token
            });
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous] // Permite acceso sin autenticación
        public async Task<ActionResult<UserResponseDto>> RegisterAsync([FromBody] UserRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userResponse = await _authService.RegisterAsync(dto);
            var token = _authService.GenerateJwt(userResponse);
            return Ok(new
            {
                message = "User registered successfully",
                User = userResponse,
                Token = token
            });
        }
    }
}