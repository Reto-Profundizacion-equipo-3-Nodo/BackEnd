using FundacionAntivirus.Dto;
using FundacionAntivirus.Services;
using FundacionAntivirus.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionDto>>> GetAllInstitutions()
        {
            var institutions = await _institutionService.GetAllInstitutions();
            return Ok(institutions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionDto>> GetInstitutionById(int id)
        {
            var institutionDto = await _institutionService.GetInstitutionById(id);
            if (institutionDto == null) return NotFound(new { message = "Institución no encontrada" });

            return Ok(institutionDto);
        }

        [HttpPost]
        public async Task<ActionResult<InstitutionDto>> CreateInstitution([FromBody] InstitutionDto institutionDto)
        {
            var createdInstitution = await _institutionService.CreateInstitution(institutionDto);
            return CreatedAtAction(nameof(GetInstitutionById), new { id = createdInstitution.Id }, createdInstitution);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InstitutionDto>> UpdateInstitution(int id, [FromBody] InstitutionDto institutionDto)
        {
            var updatedInstitution = await _institutionService.UpdateInstitution(id, institutionDto);
            if (updatedInstitution == null) return NotFound(new { message = "Institución no encontrada" });

            return Ok(updatedInstitution);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInstitution(int id)
        {
            bool deleted = await _institutionService.DeleteInstitution(id);
            if (!deleted) return NotFound(new { message = "Institución no encontrada" });

            return Ok(new { message = "Institución eliminada correctamente" });
        }
    }
}
