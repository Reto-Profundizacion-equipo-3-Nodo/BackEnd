namespace FundAntivirus.Services
{
    using FundAntivirus.DTOs.Opportunity;

    /// <summary>
    /// Interfaz que define los métodos del servicio de oportunidades.
    /// </summary>
    public interface IOpportunityService
    {
        /// <summary>
        /// Obtiene todas las oportunidades.
        /// </summary>
        /// <returns>Lista de oportunidades.</returns>
        Task<IEnumerable<OpportunityResponseDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Oportunidad encontrada.</returns>
        Task<OpportunityResponseDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="dto">Datos de la oportunidad a crear.</param>
        /// <returns>Oportunidad creada.</returns>
        Task<OpportunityResponseDTO> CreateAsync(CreateOpportunityDTO dto);

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <param name="dto">Datos actualizados.</param>
        /// <returns>Oportunidad actualizada.</returns>
        Task<OpportunityResponseDTO?> UpdateAsync(int id, UpdateOpportunityDTO dto);

        /// <summary>
        /// Elimina una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Verdadero si se eliminó correctamente.</returns>
        Task<bool> DeleteAsync(int id);
    }
}