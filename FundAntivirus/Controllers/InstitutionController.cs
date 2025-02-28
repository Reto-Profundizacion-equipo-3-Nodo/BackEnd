using FundAntivirus.DTOs;
using FundAntivirus.Models;
using FundAntivirus.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundAntivirus.Controllers
{
    [Route("api/institutions")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetAllInstitutions()
        {
            return Ok(await _institutionService.GetAllInstitutions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> GetInstitutionById(int id)
        {
            var institution = await _institutionService.GetInstitutionById(id);
            if (institution == null) return NotFound(new { message = "Institución no encontrada" });

            return Ok(institution);
        }

        [HttpPost]
        public async Task<ActionResult<Institution>> CreateInstitution([FromBody] CreateInstitutionDto institutionDto)
        {
            var institution = await _institutionService.CreateInstitution(institutionDto);
            return CreatedAtAction(nameof(GetInstitutionById), new { id = institution.Id }, institution);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Institution>> UpdateInstitution(int id, [FromBody] UpdateInstitutionDto institutionDto)
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