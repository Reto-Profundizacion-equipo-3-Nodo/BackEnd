using FundacionAntivirus.Dtos;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICategoryService _categoryService;
        private readonly IInstitutionService _institutionService;

        /// <summary>
        /// Constructor del controlador de oportunidades.
        /// </summary>
        public OpportunityController(IOpportunityService opportunityService, ICategoryService categoryService, IInstitutionService institutionService)
        {
            _opportunityService = opportunityService;
            _categoryService = categoryService;
            _institutionService = institutionService;
        }

        /// <summary>
        /// Obtiene todas las oportunidades.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "admin,user")] // Admin y User pueden ver todas las oportunidades
        public async Task<ActionResult<IEnumerable<OpportunityResponseDto>>> GetAll()
        {
            var opportunities = await _opportunityService.GetAllOpportunitiesAsync();
            var opportunityDtos = opportunities.Select(opportunity => new OpportunityResponseDto
            {
                Id = opportunity.Id,
                Name = opportunity.Name,
                Observation = opportunity.Observation,
                Type = opportunity.Type,
                Description = opportunity.Description,
                Requires = opportunity.Requires,
                Guide = opportunity.Guide,
                AdditionalDates = opportunity.AdditionalDates,
                ServiceChannels = opportunity.ServiceChannels,
                Manager = opportunity.Manager,
                Modality = opportunity.Modality,
                CategoryId = opportunity.CategoryId,
                InstitutionId = opportunity.InstitutionId,
                CategoryName = opportunity.Category?.Name // Asumiendo que tienes la categoría relacionada
            });

            return Ok(opportunityDtos);
        }

        /// <summary>
        /// Obtiene una oportunidad por ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")] // Admin y User pueden ver una oportunidad específica
        public async Task<ActionResult<OpportunityResponseDto>> GetById(int id)
        {
            var opportunity = await _opportunityService.GetOpportunityByIdAsync(id);
            if (opportunity == null) return NotFound();

            var opportunityDto = new OpportunityResponseDto
            {
                Id = opportunity.Id,
                Name = opportunity.Name,
                Observation = opportunity.Observation,
                Type = opportunity.Type,
                Description = opportunity.Description,
                Requires = opportunity.Requires,
                Guide = opportunity.Guide,
                AdditionalDates = opportunity.AdditionalDates,
                ServiceChannels = opportunity.ServiceChannels,
                Manager = opportunity.Manager,
                Modality = opportunity.Modality,
                CategoryId = opportunity.CategoryId,
                InstitutionId = opportunity.InstitutionId,
                CategoryName = opportunity.Category?.Name // Asumiendo que tienes la categoría relacionada
            };

            return Ok(opportunityDto);
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "admin")] // Solo los administradores pueden crear oportunidades
        public async Task<ActionResult<OpportunityResponseDto>> Create([FromBody] OpportunityCreateDto opportunityCreateDto)
        {
            // Validar que la categoría exista
            if (opportunityCreateDto.CategoryId.HasValue)
            {
                var category = await _categoryService.GetCategoryByIdAsync(opportunityCreateDto.CategoryId.Value);
                if (category == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        StatusCode = 400,
                        Message = "La categoría no existe.",
                        RequestId = HttpContext.TraceIdentifier
                    });
                }
            }
            // Validar que la institución exista
            if (opportunityCreateDto.InstitutionId.HasValue)
            {
                var institution = await _institutionService.GetInstitutionById(opportunityCreateDto.InstitutionId.Value);
                if (institution == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        StatusCode = 400,
                        Message = "La institución no existe.",
                        RequestId = HttpContext.TraceIdentifier
                    });
                }
            }

            var opportunity = new Opportunity
            {
                Name = opportunityCreateDto.Name,
                Observation = opportunityCreateDto.Observation,
                Type = opportunityCreateDto.Type,
                Description = opportunityCreateDto.Description,
                Requires = opportunityCreateDto.Requires,
                Guide = opportunityCreateDto.Guide,
                AdditionalDates = opportunityCreateDto.AdditionalDates,
                ServiceChannels = opportunityCreateDto.ServiceChannels,
                Manager = opportunityCreateDto.Manager,
                Modality = opportunityCreateDto.Modality,
                CategoryId = opportunityCreateDto.CategoryId,
                InstitutionId = opportunityCreateDto.InstitutionId
            };

            var createdOpportunity = await _opportunityService.CreateOpportunityAsync(opportunity);

            var createdOpportunityDto = new OpportunityResponseDto
            {
                Id = createdOpportunity.Id,
                Name = createdOpportunity.Name,
                Observation = createdOpportunity.Observation,
                Type = createdOpportunity.Type,
                Description = createdOpportunity.Description,
                Requires = createdOpportunity.Requires,
                Guide = createdOpportunity.Guide,
                AdditionalDates = createdOpportunity.AdditionalDates,
                ServiceChannels = createdOpportunity.ServiceChannels,
                Manager = createdOpportunity.Manager,
                Modality = createdOpportunity.Modality,
                CategoryId = createdOpportunity.CategoryId,
                InstitutionId = createdOpportunity.InstitutionId,
                CategoryName = createdOpportunity.Category?.Name // Asumiendo que tienes la categoría relacionada
            };

            return CreatedAtAction(nameof(GetById), new { id = createdOpportunityDto.Id }, createdOpportunityDto);
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden actualizar oportunidades
        public async Task<IActionResult> Update(int id, [FromBody] OpportunityCreateDto opportunityCreateDto)
        {
            var opportunity = new Opportunity
            {
                Id = id,
                Name = opportunityCreateDto.Name,
                Observation = opportunityCreateDto.Observation,
                Type = opportunityCreateDto.Type,
                Description = opportunityCreateDto.Description,
                Requires = opportunityCreateDto.Requires,
                Guide = opportunityCreateDto.Guide,
                AdditionalDates = opportunityCreateDto.AdditionalDates,
                ServiceChannels = opportunityCreateDto.ServiceChannels,
                Manager = opportunityCreateDto.Manager,
                Modality = opportunityCreateDto.Modality,
                CategoryId = opportunityCreateDto.CategoryId,
                InstitutionId = opportunityCreateDto.InstitutionId
            };

            var updatedOpportunity = await _opportunityService.UpdateOpportunityAsync(id, opportunity);
            if (updatedOpportunity == null) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Elimina una oportunidad por ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden eliminar oportunidades
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _opportunityService.DeleteOpportunityAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
