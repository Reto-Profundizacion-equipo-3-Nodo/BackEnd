namespace FundAntivirus.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using FundAntivirus.Services;
    using FundAntivirus.DTOs.Opportunity;

    /// <summary>
    /// Controlador para gestionar las oportunidades.
    /// </summary>
    [ApiController]
    [Route("api/opportunities")]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;

        /// <summary>
        /// Constructor del controlador que inyecta el servicio de oportunidades.
        /// </summary>
        /// <param name="opportunityService">Servicio de oportunidades.</param>
        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        /// <summary>
        /// Obtiene todas las oportunidades disponibles.
        /// </summary>
        /// <returns>Lista de oportunidades.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var opportunities = await _opportunityService.GetAllAsync();
            return Ok(opportunities);
        }

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Detalles de la oportunidad.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var opportunity = await _opportunityService.GetByIdAsync(id);
            if (opportunity == null)
                return NotFound("Oportunidad no encontrada.");
            
            return Ok(opportunity);
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="dto">Datos de la oportunidad a crear.</param>
        /// <returns>Oportunidad creada.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOpportunityDTO dto)
        {
            var createdOpportunity = await _opportunityService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.Id }, createdOpportunity);
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <param name="dto">Datos actualizados de la oportunidad.</param>
        /// <returns>Oportunidad actualizada.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOpportunityDTO dto)
        {
            var updatedOpportunity = await _opportunityService.UpdateAsync(id, dto);
            if (updatedOpportunity == null)
                return NotFound("Oportunidad no encontrada.");
            
            return Ok(updatedOpportunity);
        }

        /// <summary>
        /// Elimina una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Estado de la operación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _opportunityService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Oportunidad no encontrada.");
            
            return NoContent();
        }
    }
}
