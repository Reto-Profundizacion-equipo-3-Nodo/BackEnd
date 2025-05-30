using FundacionAntivirus.Dtos;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _service;
        private readonly IAuthService _authService;

        public UsersController(IUser service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden ver todos los usuarios
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            try
            {
                var list = await _service.GetAllAsync();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorViewModel
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor al obtener la lista de usuarios",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")] // Tanto Admin como User pueden ver un usuario por ID
        public async Task<ActionResult<UserResponseDto>> Get(int id)
        {
            try
            {
                var dto = await _service.GetByIdAsync(id);
                if (dto == null)
                {
                    return StatusCode(404, new ErrorViewModel
                    {
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        RequestId = HttpContext.TraceIdentifier
                    });
                }
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorViewModel
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor al obtener el usuario",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden crear usuarios
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _service.CreateUserAsync(dto);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorViewModel
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor al crear el usuario",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPut("Updatebyid/{id}")]
        [Authorize(Roles = "admin,user")] // Admin puede actualizar cualquier usuario, User solo su propio perfil
        public async Task<ActionResult> Update(int id, [FromBody] UserRequestDto dto)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                {
                    return StatusCode(404, new ErrorViewModel
                    {
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        RequestId = HttpContext.TraceIdentifier
                    });
                }

                if (User.IsInRole("User") && User.Identity.Name != entity.Email)
                {
                    return Forbid(); // Un usuario no puede modificar a otro usuario
                }

                if (string.IsNullOrEmpty(dto.Name) || dto.Name == "string")
                {
                    return BadRequest(new { message = "Name is required" });
                }
                if (string.IsNullOrEmpty(dto.Email) || dto.Email == "user@example.com" || !dto.Email.Contains("@"))
                {
                    return BadRequest(new { message = "Email is required or email is not valid" });
                }
                var allowedRoles = new List<string> { "admin", "user" };
                if (!allowedRoles.Contains(dto.Rol))
                {
                    return BadRequest(new { message = "Role is not valid" });
                }
                await _service.UpdateUserAsync(id, dto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorViewModel
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor al actualizar el usuario",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden eliminar usuarios
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(404, new ErrorViewModel
                    {
                        StatusCode = 404,
                        Message = "Usuario no encontrado",
                        RequestId = HttpContext.TraceIdentifier
                    });
                }
                await _service.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorViewModel
                {
                    StatusCode = 500,
                    Message = "Error interno del servidor al eliminar el usuario",
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }
    }
}
