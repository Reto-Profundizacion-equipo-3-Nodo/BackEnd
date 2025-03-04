using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de oportunidades.
    /// </summary>
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly ILogger<OpportunityService> _logger;

        /// <summary>
        /// Constructor de OpportunityService.
        /// </summary>
        /// <param name="opportunityRepository">Repositorio de oportunidades.</param>
        /// <param name="logger">Logger para registrar eventos y errores.</param>
        public OpportunityService(IOpportunityRepository opportunityRepository, ILogger<OpportunityService> logger)
        {
            _opportunityRepository = opportunityRepository ?? throw new ArgumentNullException(nameof(opportunityRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtiene todas las oportunidades disponibles.
        /// </summary>
        /// <returns>Lista de oportunidades.</returns>
        public async Task<IEnumerable<Opportunity>> GetAllOpportunitiesAsync()
        {
            try
            {
                return await _opportunityRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las oportunidades.");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Oportunidad encontrada o null.</returns>
        public async Task<Opportunity?> GetOpportunityByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("El ID proporcionado no es válido: {Id}", id);
                return null;
            }

            try
            {
                return await _opportunityRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la oportunidad con ID {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="opportunity">Objeto Opportunity a crear.</param>
        /// <returns>Oportunidad creada.</returns>
        public async Task<Opportunity> CreateOpportunityAsync(Opportunity opportunity)
        {
            if (opportunity == null)
            {
                throw new ArgumentNullException(nameof(opportunity), "La oportunidad no puede ser nula.");
            }

            try
            {
                return await _opportunityRepository.AddAsync(opportunity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva oportunidad.");
                throw;
            }
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <param name="opportunity">Objeto Opportunity con los nuevos datos.</param>
        /// <returns>Oportunidad actualizada o null si no se encuentra.</returns>
        public async Task<Opportunity?> UpdateOpportunityAsync(int id, Opportunity opportunity)
        {
            if (id <= 0 || opportunity == null)
            {
                _logger.LogWarning("Parámetros inválidos para actualizar la oportunidad con ID {Id}", id);
                return null;
            }

            try
            {
                return await _opportunityRepository.UpdateAsync(id, opportunity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la oportunidad con ID {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Elimina una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False si no.</returns>
        public async Task<bool> DeleteOpportunityAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Intento de eliminar una oportunidad con un ID no válido: {Id}", id);
                return false;
            }

            try
            {
                return await _opportunityRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la oportunidad con ID {Id}", id);
                throw;
            }
        }
    }
}
