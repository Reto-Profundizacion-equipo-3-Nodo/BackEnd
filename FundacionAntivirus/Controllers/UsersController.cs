
using FundacionAntivirus.Dto;
using FundacionAntivirus.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _service;
        private readonly IAuthService _authService;

        public UsersController(IUsers service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UsersResponseDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersResponseDto>> Get(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<ActionResult<UsersResponseDto>> Create([FromBody] UsersRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.CreateAsync(dto);
            return Ok(new { message = "User registered successfully" });
            // return CreatedAtAction(nameof(Get), dto);
        }

        [HttpPut("{Updatebyid}")]
        public async Task<ActionResult> Update(int id, [FromBody] UsersRequestDto dto)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}