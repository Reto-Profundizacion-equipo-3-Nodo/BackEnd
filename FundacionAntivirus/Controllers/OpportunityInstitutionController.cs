using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityInstitutionController : ControllerBase
    {
        private readonly IOpportunityInstitutionService _service;

        public OpportunityInstitutionController(IOpportunityInstitutionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpportunityInstitution>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpportunityInstitution>> GetById(int id)
        {
            var institution = await _service.GetByIdAsync(id);
            if (institution == null) return NotFound();
            return Ok(institution);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OpportunityInstitution>> Create(OpportunityInstitution institution)
        {
            var newInstitution = await _service.AddAsync(institution);
            return CreatedAtAction(nameof(GetById), new { id = newInstitution.Id }, newInstitution);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OpportunityInstitution>> Update(int id, OpportunityInstitution institution)
        {
            var updatedInstitution = await _service.UpdateAsync(id, institution);
            if (updatedInstitution == null) return NotFound();
            return Ok(updatedInstitution);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
