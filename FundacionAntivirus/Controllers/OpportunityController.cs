using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        /// <summary>
        /// Constructor para inyectar el servicio de oportunidades.
        /// </summary>
        /// <param name="opportunityService">Servicio de oportunidad</param>
        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        /// <summary>
        /// Obtiene todas las oportunidades.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var opportunities = await _opportunityService.GetAllAsync();
                return Ok(opportunities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var opportunity = await _opportunityService.GetByIdAsync(id);
                if (opportunity == null)
                    return NotFound(new { message = "Oportunidad no encontrada" });

                return Ok(opportunity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="opportunity">Objeto oportunidad</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Opportunity opportunity)
        {
            try
            {
                if (opportunity == null)
                    return BadRequest(new { message = "Datos inválidos" });

                var createdOpportunity = await _opportunityService.CreateAsync(opportunity);
                return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.Id }, createdOpportunity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad</param>
        /// <param name="opportunity">Objeto oportunidad actualizado</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Opportunity opportunity)
        {
            try
            {
                if (id != opportunity.Id)
                    return BadRequest(new { message = "El ID no coincide" });

                var updatedOpportunity = await _opportunityService.UpdateAsync(id, opportunity);
                if (updatedOpportunity == null)
                    return NotFound(new { message = "Oportunidad no encontrada" });

                return Ok(updatedOpportunity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina una oportunidad por ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _opportunityService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Oportunidad no encontrada" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
