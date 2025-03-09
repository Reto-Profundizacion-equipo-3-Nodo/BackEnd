using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    /// <summary>
    /// Controlador para gestionar oportunidades.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        /// <summary>
        /// Constructor del controlador de oportunidades.
        /// </summary>
        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        /// <summary>
        /// Obtiene todas las oportunidades.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opportunity>>> GetAll()
        {
            return Ok(await _opportunityService.GetAllOpportunitiesAsync());
        }

        /// <summary>
        /// Obtiene una oportunidad por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Opportunity>> GetById(int id)
        {
            var opportunity = await _opportunityService.GetOpportunityByIdAsync(id);
            if (opportunity == null) return NotFound();
            return Ok(opportunity);
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Opportunity>> Create([FromBody] Opportunity opportunity)
        {
            var createdOpportunity = await _opportunityService.CreateOpportunityAsync(opportunity);
            return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.Id }, createdOpportunity);
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Opportunity opportunity)
        {
            var updatedOpportunity = await _opportunityService.UpdateOpportunityAsync(id, opportunity);
            if (updatedOpportunity == null) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Elimina una oportunidad por ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _opportunityService.DeleteOpportunityAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}