using FundAntivirus.Interfaces;
using FundAntivirus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAntivirus.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        //POST(Crear usuario)
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var (success, message) = await _adminService.CreateAsync(user);
                if (!success) return BadRequest(new { message });

                return CreatedAtAction(nameof(Get), new { id = user.Id }, new { message });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Error Inesperado" + ex.Message });
            }
        }

        //GET(Obtener usuarios por id)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var User = await _adminService.GetByIdAsync(id);
            if (User == null) return NotFound("User not found");
            return Ok(User);

        }

        //GET(Obtener todos los usuarios)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var User = await _adminService.GetAllAsync();
            return Ok(User);
        }

        //PUT(Actualizar usuario)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto user)
        {
            var User = await _adminService.UpdateAsync(id, user);
            if (User == null) return NotFound("User not found");
            return Ok(User);
        }

        //DELETE(Eliminar usuario)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _adminService.GetByIdAsync(id);
            if (existingUser == null) return NotFound("User not found");
            await _adminService.DeleteAsync(id);
            return Ok();
        }

    }
}