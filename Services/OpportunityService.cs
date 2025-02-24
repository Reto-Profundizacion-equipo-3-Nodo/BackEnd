namespace FundAntivirus.Services
{
    using FundAntivirus.DTOs.Opportunity;
    using FundAntivirus.Models;
    using FundAntivirus.Repositories;

    /// <summary>
    /// Servicio para gestionar la lógica de negocio de las oportunidades.
    /// </summary>
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;

        /// <summary>
        /// Constructor que inyecta el repositorio de oportunidades.
        /// </summary>
        /// <param name="opportunityRepository">Repositorio de oportunidades.</param>
        public OpportunityService(IOpportunityRepository opportunityRepository)
        {
            _opportunityRepository = opportunityRepository;
        }

        /// <summary>
        /// Obtiene todas las oportunidades.
        /// </summary>
        /// <returns>Lista de oportunidades.</returns>
        public async Task<IEnumerable<OpportunityResponseDTO>> GetAllAsync()
        {
            return await _opportunityRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Oportunidad encontrada.</returns>
        public async Task<OpportunityResponseDTO?> GetByIdAsync(int id)
        {
            return await _opportunityRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="dto">Datos de la oportunidad a crear.</param>
        /// <returns>Oportunidad creada.</returns>
        public async Task<OpportunityResponseDTO> CreateAsync(CreateOpportunityDTO dto)
        {
            return await _opportunityRepository.CreateAsync(dto);
        }

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <param name="dto">Datos actualizados.</param>
        /// <returns>Oportunidad actualizada.</returns>
        public async Task<OpportunityResponseDTO?> UpdateAsync(int id, UpdateOpportunityDTO dto)
        {
            return await _opportunityRepository.UpdateAsync(id, dto);
        }

        /// <summary>
        /// Elimina una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Verdadero si se eliminó correctamente.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await _opportunityRepository.DeleteAsync(id);
        }
    }
}
