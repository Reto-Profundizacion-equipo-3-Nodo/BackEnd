using FundacionAntivirus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Interfaces
{
    /// <summary>
    /// Interface para el repositorio de oportunidades.
    /// Define las operaciones básicas de acceso a datos para las oportunidades.
    /// </summary>
    public interface IOpportunityRepository
    {
        /// <summary>
        /// Obtiene todas las oportunidades disponibles en la base de datos.
        /// </summary>
        /// <returns>Una lista de oportunidades.</returns>
        Task<IEnumerable<Opportunity>> GetAllAsync();

        /// <summary>
        /// Obtiene una oportunidad específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la oportunidad.</param>
        /// <returns>La oportunidad correspondiente o null si no se encuentra.</returns>
        Task<Opportunity?> GetByIdAsync(int id);

        /// <summary>
        /// Agrega una nueva oportunidad a la base de datos.
        /// </summary>
        /// <param name="opportunity">El objeto de oportunidad a agregar.</param>
        /// <returns>La oportunidad creada con su ID asignado.</returns>
        Task<Opportunity> AddAsync(Opportunity opportunity);

        /// <summary>
        /// Actualiza una oportunidad existente en la base de datos.
        /// </summary>
        /// <param name="id">El ID de la oportunidad a actualizar.</param>
        /// <param name="opportunity">El objeto de oportunidad con los datos actualizados.</param>
        /// <returns>La oportunidad actualizada o null si no se encontró.</returns>
        Task<Opportunity?> UpdateAsync(int id, Opportunity opportunity);

        /// <summary>
        /// Elimina una oportunidad de la base de datos por su ID.
        /// </summary>
        /// <param name="id">El ID de la oportunidad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}