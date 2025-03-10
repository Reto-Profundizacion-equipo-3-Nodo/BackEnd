using Antivirus.Models.Dtos;
using Antivirus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Antivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BootcampController : ControllerBase
    {
        private readonly IBootcampService _bootcampService;

        public BootcampController(IBootcampService bootcampService)
        {
            _bootcampService = bootcampService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BootcampDto>>> GetAll()
        {
            var bootcamps = await _bootcampService.GetAllAsync();
            return Ok(bootcamps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BootcampDto>> GetById(int id)
        {
            var bootcamp = await _bootcampService.GetByIdAsync(id);
            if (bootcamp == null) return NotFound();
            return Ok(bootcamp);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BootcampCreateDto bootcampDto)
        {
            await _bootcampService.CreateAsync(Dto);
            return CreatedAtAction(nameof(GetAll), Dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BootcampCreateDto bootcampDto)
        {
            var bootcamp = await _bootcampService.UpdateAsync(id, bootcampDto);
            if (bootcamp == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bootcampService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        {
            var result = await _bootcampService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}