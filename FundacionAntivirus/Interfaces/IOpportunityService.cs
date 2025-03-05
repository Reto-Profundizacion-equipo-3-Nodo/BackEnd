using System.Collections.Generic;
using System.Threading.Tasks;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Interfaces
{
    /// <summary>
    /// Define las operaciones relacionadas con la gestión de oportunidades.
    /// </summary>
    public interface IOpportunityService
    {
        /// <summary>
        /// Obtiene todas las oportunidades disponibles.
        /// </summary>
        /// <returns>Lista de oportunidades.</returns>
        Task<IEnumerable<Opportunity>> GetAllOpportunitiesAsync();

        /// <summary>
        /// Obtiene una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad.</param>
        /// <returns>Objeto de tipo Opportunity.</returns>
        Task<Opportunity?> GetOpportunityByIdAsync(int id);

        /// <summary>
        /// Crea una nueva oportunidad.
        /// </summary>
        /// <param name="opportunity">Objeto de oportunidad a crear.</param>
        /// <returns>La oportunidad creada.</returns>
        Task<Opportunity> CreateOpportunityAsync(Opportunity opportunity);

        /// <summary>
        /// Actualiza una oportunidad existente.
        /// </summary>
        /// <param name="id">ID de la oportunidad a actualizar.</param>
        /// <param name="opportunity">Objeto de oportunidad con los datos actualizados.</param>
        /// <returns>La oportunidad actualizada o null si no se encontró.</returns>
        Task<Opportunity?> UpdateOpportunityAsync(int id, Opportunity opportunity);

        /// <summary>
        /// Elimina una oportunidad por su ID.
        /// </summary>
        /// <param name="id">ID de la oportunidad a eliminar.</param>
        /// <returns>True si se eliminó correctamente, False en caso contrario.</returns>
        Task<bool> DeleteOpportunityAsync(int id);
    }
}
